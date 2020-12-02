using System;

namespace InterestRateVigilant.Tests
{

    /*
        TESTING APPLICATIONS WOULD HAVE THE FOLLOWING FRAMEWORKS:
        1- Test Framework. It helps us with attributes and functions to make testing applications more readable and maintainable.  
        2- Test Runner Framework. The test runner executes our test code. The test runner calls our test framework.
        3- Test Assertion Framework. It helps us to assert results from the test code.
        4- Test Mocking Framework. It is used to define te boundaries of your system under test (SUT)
     */

    class Program
    {
        static void Main(string[] args)
        {
            var test = new TestFramework();

            foreach (var testData in test.CustomerIsNoneVipTestDataCollection)
            {
                test.ExpectedRateForCustomerAccountTest(testData);
            }

            foreach (var testData in test.CustomerIsVipTestDataCollection)
            {
                test.ExpectedRateForCustomerAccountTest(testData);
            }

            test.EmailSentToBrokerWhenCustomerIsVipTest();
            test.UnknownAccountThrowsExceptionWhenCustomerVipTest();
            test.UnknownAccountThrowsExceptionWhenCustomerNotVipTest();

            Console.ReadLine();
        }
    }
}
