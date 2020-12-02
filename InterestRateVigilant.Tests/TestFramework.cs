using System;
using System.Collections.Generic;

namespace InterestRateVigilant.Tests
{
    internal class TestFramework
    {
        internal class TestData
        {
            public bool CustomerIsVip { get; set; }
            public AccountTypes AccountType { get; set; }
            public decimal ExpectedRate { get; set; }
        }

        internal List<TestData> CustomerIsNoneVipTestDataCollection = new List<TestData>
        {
            new TestData{ AccountType = AccountTypes.Savings, CustomerIsVip = false, ExpectedRate = 1.05m },
            new TestData{ AccountType = AccountTypes.Current, CustomerIsVip = false, ExpectedRate = 0.55m },
            new TestData{ AccountType = AccountTypes.Simple, CustomerIsVip = false, ExpectedRate = 1.90m },
        };

        internal List<TestData> CustomerIsVipTestDataCollection = new List<TestData>
        {
            new TestData{ AccountType = AccountTypes.Savings, CustomerIsVip = true, ExpectedRate = 11.00m },
            new TestData{ AccountType = AccountTypes.Current, CustomerIsVip = true, ExpectedRate = 5.25m },
            new TestData{ AccountType = AccountTypes.Simple, CustomerIsVip = true, ExpectedRate = 2.75m },
        };

        internal void ExpectedRateForCustomerAccountTest(TestData testData)
        {
            // Arrange
            var fakeCustomerRepo = Fakes.GetFakeCustomerRepository(isVip: testData.CustomerIsVip);
            var fakeAccountRepo = Fakes.GetFakeAccountRepository(testData.AccountType);
            var fakeEmailSrv = Fakes.GetFakeEmailService();

            var interestManager = new InterestVigilant(fakeCustomerRepo, fakeAccountRepo, fakeEmailSrv);

            // Act
            var actual = interestManager.CalculateInterestForAccountCustomer(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            Assert.AreEqual(testData.ExpectedRate, actual.Rate);
        }

        internal void EmailSentToBrokerWhenCustomerIsVipTest()
        {
            // Arrange
            var expectedBrokerId = Guid.NewGuid();
            var expectedCustomerId = Guid.NewGuid();
            var expectedAccountId = Guid.NewGuid();

            var fakeCustomerRepo = Fakes.GetFakeCustomerRepository(isVip: true);
            var fakeAccountRepo = Fakes.GetFakeAccountRepository(AccountTypes.Current, expectedBrokerId);
            var fakeEmailSrv = Fakes.GetFakeEmailService();

            var interestManager = new InterestVigilant(fakeCustomerRepo, fakeAccountRepo, fakeEmailSrv);

            // Act
            interestManager.CalculateInterestForAccountCustomer(expectedCustomerId, expectedAccountId);

            // Assert
            Assert.WasCalledWithParameters(fakeEmailSrv, expectedBrokerId, expectedAccountId, 5.25m);
        }

        internal void UnknownAccountThrowsExceptionWhenCustomerVipTest()
        {
            UnknownAccountThrowsExceptionTest(true);
        }

        internal void UnknownAccountThrowsExceptionWhenCustomerNotVipTest()
        {
            UnknownAccountThrowsExceptionTest(false);
        }

        internal void UnknownAccountThrowsExceptionTest(bool isCustomerVip)
        {
            // Assert
            var fakeCustomerRepo = Fakes.GetFakeCustomerRepository(isVip: isCustomerVip);
            var fakeAccountRepo = Fakes.GetFakeAccountRepository(AccountTypes.Join);
            var fakeEmailSrv = Fakes.GetFakeEmailService();

            var interestManager = new InterestVigilant(fakeCustomerRepo, fakeAccountRepo, fakeEmailSrv);

            // Act & Assert
            Assert.ThrowsException(() => 
            {
                interestManager.CalculateInterestForAccountCustomer(Guid.NewGuid(), Guid.NewGuid());
            });
        }
    }
}