namespace BlueHarvest.Core.Exceptions;

public class ArgumentPropertyNullException : Exception
{
   public ArgumentPropertyNullException()
   {
   }

   public ArgumentPropertyNullException(string message)
      : base(message)
   {
   }

   public ArgumentPropertyNullException(string message, Exception inner)
      : base(message, inner)
   {
   }

   public static ArgumentPropertyNullException Create(string argumentName, string propertyName) =>
      new ArgumentPropertyNullException($"Property '{propertyName}' of argument '{argumentName}' is null.");
}
