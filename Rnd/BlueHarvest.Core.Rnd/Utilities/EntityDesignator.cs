namespace BlueHarvest.Core.Rnd.Utilities;

public interface IEntityDesignator
{
   const string DefaultTemplate = @"AAA-AAA";

   string Generate(string template = DefaultTemplate);
}

public class EntityDesignator : IEntityDesignator
{
   private readonly IRng _rng;

   private const string Letters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   private const string Numbers = @"0123456789";
   private const string AlphaNumeric = Letters + Numbers;

   public static readonly IEntityDesignator Default = new EntityDesignator(new SimpleRng());
   
   public EntityDesignator(IRng rng)
   {
      _rng = rng;
   }

   public string Generate(string template = IEntityDesignator.DefaultTemplate)
   {
      var result = string.Empty;

      foreach (var t in template)
      {
         switch (t)
         {
            case 'L':
               result += Letters[_rng.Next(0, Letters.Length)];
               break;
            case 'N':
               result += Numbers[_rng.Next(0, Numbers.Length)];
               break;
            case 'A':
               result += AlphaNumeric[_rng.Next(0, AlphaNumeric.Length)];
               break;
            default:
               result += t;
               break;
         }
      }

      return result;
   }
}
