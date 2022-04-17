namespace BlueHarvest.Core.Extensions;

public static class ExceptionExtensions
{
   public static object? GetDeepDetail(this Exception? ex)
   {
      if (ex == null)
         return null;

      var detail = new
      {
         Message = ex?.Message,
         Source = ex?.Source,
         StackTrace = ex?.StackTrace?.Split(Environment.NewLine),
         Inner = ex?.InnerException?.GetDeepDetail()
      };

      return detail;
   }
}
