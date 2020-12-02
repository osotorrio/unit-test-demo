using System;
using System.Diagnostics;

namespace InterestRateVigilant.Tests
{
    internal class Assert
    {
        internal static void AreEqual(decimal expected, decimal actual)
        {
            StackTrace stackTrace = new StackTrace();
            var testName = stackTrace.GetFrame(1).GetMethod().Name;

            if (expected == actual)
            {
                Console.WriteLine($"{testName} passed. Expected {expected}. Actual {actual}");
            }
            else
            {
                Console.WriteLine($"{testName} FAILED!!. Expected {expected}. Actual {actual}");
            }
        }

        internal static void WasCalledWithParameters(Fakes.FakeEmailService fakeEmailSrv, Guid expectedBrokerId, Guid expectedAccountId, decimal expectedRate)
        {
            StackTrace stackTrace = new StackTrace();
            var testName = stackTrace.GetFrame(1).GetMethod().Name;
            var fakeName = fakeEmailSrv.GetType().Name;

            if (fakeEmailSrv.WasCalled)
            {
                Console.WriteLine($"{testName} passed. {fakeName} was called");
            }
            else
            {
                Console.WriteLine($"{testName} FAILED!!. Expected call to {fakeName} but it never happened");
            }

            if (expectedBrokerId != fakeEmailSrv.BrokerId)
            {
                Console.WriteLine($"{testName} FAILED!!. Expected call to {fakeName} with BrokerId {expectedBrokerId} but actual was {fakeEmailSrv.BrokerId}");
            }

            if (expectedAccountId != fakeEmailSrv.AccountId)
            {
                Console.WriteLine($"{testName} FAILED!!. Expected call to {fakeName} with AccountId {expectedAccountId} but actual was {fakeEmailSrv.AccountId}");
            }

            if (expectedRate != fakeEmailSrv.Rate)
            {
                Console.WriteLine($"{testName} FAILED!!. Expected call to {fakeName} with Rate {expectedRate} but actual was {fakeEmailSrv.Rate}");
            }
        }

        internal static void ThrowsException(Action methodCall)
        {
            StackTrace stackTrace = new StackTrace();
            var testName = stackTrace.GetFrame(2).GetMethod().Name;

            try
            {
                methodCall();
                Console.WriteLine($"{testName} FAILED!!. Expected to throw an exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{testName} passed. Exception was thrown");

                var expectedStringMessage = "Interest type unknown: ";

                if (!ex.Message.Contains(expectedStringMessage))
                {
                    Console.WriteLine($"{testName} FAILED!!. Expected exception to contain: {expectedStringMessage}. But actual was: {ex.Message}");
                }
            }
        }
    }
}