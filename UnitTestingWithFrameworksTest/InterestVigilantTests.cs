using InterestRateVigilant;
using NSubstitute;
using Shouldly;
using System;
using Xunit;

namespace UnitTestingWithFrameworksTest
{
    public class InterestVigilantTests
    {
        [Fact]
        public void CustomerNoVipAccountSavingsTest()
        {
            // Arrange
            var fakeCustomerRepo = Substitute.For<ICustomerRepository>();
            var fakeAccountRepo = Substitute.For<IAccountRepository>();
            var fakeEmailSrc = Substitute.For<IEmailService>();

            var customer = new Customer
            {
                IsVip = false
            };

            fakeCustomerRepo.GetCustomerById(Arg.Any<Guid>()).Returns(customer);

            fakeAccountRepo.GetAccountById(Arg.Any<Guid>()).Returns(new Account { AccountType = AccountTypes.Savings});

            var rateManager = new InterestVigilant(fakeCustomerRepo, fakeAccountRepo, fakeEmailSrc);

            // Act
            var response = rateManager.CalculateInterestForAccountCustomer(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            response.Rate.ShouldBe(1.05m);
        }
    }
}
