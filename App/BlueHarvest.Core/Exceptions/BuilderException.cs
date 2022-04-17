namespace BlueHarvest.Core.Exceptions;

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

   public static BuilderException CreateTooManyInterstellarObjects(long maxPossible, long count) =>
      new BuilderException($"Too many Interstellar Objects requested. Max possible: {maxPossible}, amount specified in options: {count}");
}
