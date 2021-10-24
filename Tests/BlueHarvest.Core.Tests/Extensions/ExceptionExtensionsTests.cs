using System;
using BlueHarvest.Core.Extensions;
using NUnit.Framework;

namespace BlueHarvest.Core.Tests.Extensions
{
   public class ExceptionExtensionsTests
   {
      [Test]
      public void WhenException_IsNull_GetDeepDetail_ReturnsNull()
      {
         Exception ex = null;

         var test = ex.GetDeepDetail();

         Assert.Null(test);
      }

      [Test]
      public void WhenException_IsNotNull_GetDeepDetail_ReturnsNoneNull()
      {
         Exception test = null;
         // NOTE: It is very late, is there a better way to generate exception for testing?
         try
         {
            throw new Exception();
         }
         catch (Exception ex)
         {
            test = ex;
         }

         var result = test.GetDeepDetail();

         Assert.NotNull(result);
      }

      [Test]
      public void WhenException_IsNotNull_GetDeepDetail_ReturnsDetailWithMessage()
      {
         Exception test = null;
         // NOTE: It is very late, is there a better way to generate exception for testing?
         try
         {
            throw new Exception("Error Message");
         }
         catch (Exception ex)
         {
            test = ex;
         }

         var result = test.GetDeepDetail();
         var msg = result.GetType().GetProperty("Message").GetValue(result, null).ToString();

         Assert.IsNotEmpty(msg);
      }

      [Test]
      public void WhenException_HasInner_GetDeepDetail_ReturnsDetailWithInner()
      {
         Exception test = null;
         // NOTE: It is very late, is there a better way to generate exception for testing?
         try
         {
            try
            {
               throw new Exception("Inner exception.");
            }
            catch (Exception ex)
            {
               throw new Exception("Error Message", ex);
            }
         }
         catch (Exception ex)
         {
            test = ex;
         }

         var result = test.GetDeepDetail();
         var inner = result.GetType().GetProperty("Inner").GetValue(result, null);

         Assert.NotNull(inner);
      }
   }
}
