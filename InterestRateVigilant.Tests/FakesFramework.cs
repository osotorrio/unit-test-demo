using System;

namespace InterestRateVigilant.Tests
{
    internal class Fakes
    {
        internal static FakeCustomerRepository GetFakeCustomerRepository(bool isVip)
        {
            return new FakeCustomerRepository(isVip);
        }

        internal static FakeAccountRepository GetFakeAccountRepository(AccountTypes accountType)
        {
            return new FakeAccountRepository(accountType);
        }

        internal static FakeAccountRepository GetFakeAccountRepository(AccountTypes accountType, Guid brokerId)
        {
            return new FakeAccountRepository(accountType, brokerId);
        }

        internal static FakeEmailService GetFakeEmailService()
        {
            return new FakeEmailService();
        }

        internal class FakeCustomerRepository : ICustomerRepository
        {
            private readonly bool _isVip;

            public FakeCustomerRepository(bool isVip)
            {
                _isVip = isVip;
            }

            public Customer GetCustomerById(Guid userId)
            {
                return new Customer { IsVip = _isVip };
            }
        }

        internal class FakeAccountRepository : IAccountRepository
        {
            private readonly AccountTypes _accountType;
            private readonly Guid _brokerId;

            public FakeAccountRepository(AccountTypes accountType)
            {
                _accountType = accountType;
            }

            public FakeAccountRepository(AccountTypes accountType, Guid brokerId) : this(accountType)
            {
                _accountType = accountType;
                _brokerId = brokerId;
            }

            public Account GetAccountById(Guid accountId)
            {
                return new Account
                {
                    BrokerId = _brokerId,
                    AccountType = _accountType 
                };
            }
        }

        internal class FakeEmailService : IEmailService
        {
            public bool WasCalled = false;
            public Guid BrokerId = Guid.Empty;
            public Guid AccountId = Guid.Empty;
            public decimal Rate = 0.00m;

            public void SendEmailToBroker(Guid brokerId, Guid acccountId, decimal rate)
            {
                WasCalled = true;
                BrokerId = brokerId;
                AccountId = acccountId;
                Rate = rate;
            }
        }
    }
}