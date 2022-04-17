namespace BlueHarvest.Core.Utilities
{
   public class MonikerGenerator : IMonikerGenerator
   {
      private readonly IRng _rng;

      private const string Letters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      private const string Numbers = @"0123456789";
      private const string AlphaNumeric = Letters + Numbers;

      public static readonly IMonikerGenerator Default = new MonikerGenerator(new SimpleRng());

      public MonikerGenerator(IRng rng)
      {
         _rng = rng;
      }

      public string Generate(string template = IMonikerGenerator.DefaultTemplate) =>
         template.Aggregate(string.Empty, (current, t) => current + t switch
         {
            'L' => Letters[ _rng.Next(0, Letters.Length) ],
            'N' => Numbers[ _rng.Next(0, Numbers.Length) ],
            'A' => AlphaNumeric[ _rng.Next(0, AlphaNumeric.Length) ],
            _ => t
         });
   }
}
