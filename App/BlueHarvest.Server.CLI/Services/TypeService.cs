using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal interface ITypeService : IBaseService
{
}

internal class TypeService : BaseService, ITypeService
{
   public TypeService(IConfiguration configuration, ILogger<BaseService> logger) : base(configuration, logger)
   {
   }

   protected override string Title => "Type Service";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.L, "List types", ListTypes);
   }

   private void ListTypes()
   {
      ClearScreen();
      var types = GetInherited(typeof(IDocument),
         type => type == typeof(object) /*|| type == typeof(Document)*/);
      DumpTypes(types);
      ShowContinue();
   }
   
   private static IEnumerable<Type>? GetInherited(Type baseType, Func<Type, bool>? isBaseType = null)
   {
      isBaseType ??= type => type == typeof(object);
      return Assembly.GetAssembly(baseType)
         ?.GetTypes()
         ?.Where(t => t.IsAssignableTo(baseType) && t != baseType && isBaseType(t.BaseType));
   }

   private static IEnumerable<Type>? GetInheritedClasses(Type bastType) =>
      Assembly.GetAssembly(bastType)
         ?.GetTypes()
         ?.Where(t => t.IsClass &&
                      !t.IsAbstract &&
                      t.BaseType == bastType &&
                      t.IsSubclassOf(bastType));

   private static void DumpTypes(IEnumerable<Type>? types, string indent = "")
   {
      foreach (var type in types)
      {
         WriteLine($"{indent}{type.Name} : {type?.BaseType?.Name}");

         var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
         foreach (var property in properties)
         {
            WriteLine($"{indent + "  "} {property.Name}");
         }
         
         var derivedTypes = GetInheritedClasses(type);
         DumpTypes(derivedTypes, indent + "   ");         
      }
   }
}
