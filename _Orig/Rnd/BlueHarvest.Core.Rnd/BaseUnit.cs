namespace BlueHarvest.Core.Rnd;

//public abstract class BaseUnitAsset
//{
//   public int StrengthModifier { get; set; }
//}



public abstract class BaseUnit
{
   //public IList<BaseUnitAsset> Assets { get; set; } = new List<BaseUnitAsset>();
}

public class GroundUnit : BaseUnit
{
   public int BaseStrength { get; set; }
}

public class AirUnit : BaseUnit
{
}
