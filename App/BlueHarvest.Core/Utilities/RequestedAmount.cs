namespace BlueHarvest.Core.Utilities;

public class RequestedAmount
{
   private readonly int? _exactAmount;
   private readonly MinMax<int>? _range;

   public RequestedAmount(int exactAmount)
   {
      _exactAmount = exactAmount;
      _range = null;
   }

   public RequestedAmount(int min, int max) : this(new MinMax<int>(min, max))
   {
   }

   public RequestedAmount(MinMax<int> minMax)
   {
      _exactAmount = default;
      _range = minMax;
   }

   public int GetAmount(IRng? rng = null)
   {
      if (_exactAmount.HasValue)
         return _exactAmount.Value;

      rng ??= new SimpleRng();
      return rng.Next(_range.Min, _range.Max);
   }
}
