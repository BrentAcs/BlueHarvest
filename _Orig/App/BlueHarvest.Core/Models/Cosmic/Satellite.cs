namespace BlueHarvest.Core.Models
{
   public abstract class Satellite : IRootModel
   {
      public string? Name { get; set; }
      public double? Distance { get; set; }
   }
}
