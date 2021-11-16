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
      var types = GetRootModels(typeof(IRootModel));
      DumpTypes(types);
      ShowContinue();
   }

   private static IEnumerable<Type>? GetRootModels(Type baseType) =>
      Assembly.GetAssembly(baseType)
         ?.GetTypes()
         ?.Where(t => baseType.IsAssignableFrom(t) && t.IsClass && t.BaseType == typeof(object));

   private static IEnumerable<Type>? GetInheritedClasses(Type bastType) =>
      Assembly.GetAssembly(bastType)
         ?.GetTypes()
         ?.Where(t => t.IsClass &&
                      !t.IsAbstract &&
                      t.BaseType == bastType &&
                      t.IsSubclassOf(bastType));

   private static void DumpTypes(IEnumerable<Type>? types, string indent = "")
   {
      if (types == null)
         return;

      foreach (var type in types)
      {
         var doc = "";
         if (type.IsAssignableTo(typeof(IDocument)))
         {
            doc = "[[Document]]";
         }

         // WriteLine($"{indent}+ {type.Name} : {type?.BaseType?.Name} {doc}");
         WriteLine($"{indent}+ {type.Name} {doc}");

         var properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
         foreach (var property in properties)
         {
            WriteLine($"{indent + "   "}- {property.Name}");
         }
         
         var derivedTypes = GetInheritedClasses(type);
         DumpTypes(derivedTypes, indent + "   ");         
      }
   }
}
