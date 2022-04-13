namespace BlueHarvest.Core;

public class BuilderException : Exception
{
   public BuilderException()
   {
   }

   public BuilderException(string message)
      : base(message)
   {
   }

   public BuilderException(string message, Exception inner)
      : base(message, inner)
   {
   }

   public static BuilderException CreateTooManySystems(long maxPossible, long count) =>
      throw new BuilderException($"Too many systems. Max possible: {maxPossible}, amount specified in options: {count}");
}
