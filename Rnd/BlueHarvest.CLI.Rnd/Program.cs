using System.Reflection;
using System.Text;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using static System.Console;

namespace BlueHarvest.CLI.Rnd;

class Program
{
   static async Task Main(string[] args)
   {
      var types = GetInherited(typeof(IRootModel),
         type => type == typeof(object) || type == typeof(Document));
      DumpTypes(types);

      WriteLine("Done.");
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
         var dervivedTypes = GetInheritedClasses(type);
         DumpTypes(dervivedTypes, indent + "   ");         
      }
   }
}
