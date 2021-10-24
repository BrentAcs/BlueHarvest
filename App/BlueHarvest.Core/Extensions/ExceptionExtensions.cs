using System;

namespace BlueHarvest.Core.Extensions
{
   public static class ExceptionExtensions
   {
      public static object? GetDeepDetail(this Exception? ex)
      {
         if (ex == null)
            return null;

         var detail = new
         {
            Message = ex?.Message,
            HResult = $"0x{ex?.HResult:X}",
            Source = ex?.Source,
            StackTrace = ex?.StackTrace?.Split(Environment.NewLine),
            Inner = ex?.InnerException?.GetDeepDetail()
         };

         return detail;
      }
   }
}
