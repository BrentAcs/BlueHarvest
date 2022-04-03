namespace BlueHarvest.Core.Exceptions;

public class CreateStarClusterException : Exception
{
   public CreateStarClusterException(string? message=default, Exception? inner=default)
      : base(message, inner)
   {
   }                                          
}
