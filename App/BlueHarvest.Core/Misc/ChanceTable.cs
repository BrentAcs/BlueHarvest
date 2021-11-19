namespace BlueHarvest.Core.Misc;

// NOTE: this name is TERRIBLE, please change.
public interface IChanceTable<T>
{
   int Count { get; }
   double PercentTotal { get; }
   void Add(T item, double chance = Double.MaxValue);
   void Clear();
   bool Contains(T item);
   T GetItem(double roll);
}

public class ChanceTable<T> : IChanceTable<T>
{
   private record Item<T>
   {
      public Item(T value, double chance = double.MaxValue)
      {
         Value = value;
         Chance = chance;
      }

      public T? Value { get; set; }
      public double Chance { get; set; }
   }

   private readonly IList<Item<T>> _items = new List<Item<T>>();

   public int Count => _items.Count;

   public double PercentTotal => _items.Where(item => item.Chance != Double.MaxValue).Sum(item => item.Chance);

   public void Add(T item, double chance = Double.MaxValue)
   {
      if (Contains(item))
         throw new ArgumentException("item already exists in collection.");

      if (chance == Double.MaxValue)
      {
         if (_items.Any(i => i.Chance == Double.MaxValue))
         {
            throw new ArgumentException("item with default chance exists in collection.");
         }
      }
      else
      {
         if(PercentTotal + chance > 100.00)
            throw new ArgumentException("item chance will exceed 100%.");
      }

      _items.Add(new Item<T>(item, chance));
   }

   public void Clear() =>
      _items.Clear();

   public bool Contains(T item) =>
      _items.Any(i => i.Value != null && i.Value.Equals(item));

   public T GetItem(double roll)
   {
      var sorted = _items.OrderBy(i => i.Chance).ToList();

      double current = 0;
      foreach (var item in sorted)
      {
         current += item.Chance;         
         if (roll < current)
            return item.Value;
      }

      return sorted.Last().Value;
   }
}
