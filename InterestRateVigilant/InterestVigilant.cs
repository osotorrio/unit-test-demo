using System;

namespace InterestRateVigilant
{
    public class InterestVigilant
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IEmailService _emailService;

        public InterestVigilant(ICustomerRepository customerRepo, IAccountRepository accountRepo, IEmailService emailService)
        {
            _customerRepo = customerRepo;
            _accountRepo = accountRepo;
            _emailService = emailService;
        }

        public Interest CalculateInterestForAccountCustomer(Guid customerId, Guid accountId)
        {
            var customer = _customerRepo.GetCustomerById(customerId);
            var account = _accountRepo.GetAccountById(accountId);

            var interest = new Interest();

            if (customer.IsVip)
            {
                switch (account.AccountType)
                {
                    case AccountTypes.Savings:
                        interest.Rate = 11.00m;
                        break;
                    case AccountTypes.Current:
                        interest.Rate = 5.25m;
                        break;
                    case AccountTypes.Simple:
                        interest.Rate = 2.75m;
                        break;
                    default:
                        throw new Exception($"Interest type unknown: {account.AccountType}");
                }

                _emailService.SendEmailToBroker(account.BrokerId, accountId, interest.Rate);
            }
            else
            {
                switch (account.AccountType)
                {
                    case AccountTypes.Savings:
                        interest.Rate = 1.05m;
                        break;
                    case AccountTypes.Current:
                        interest.Rate = 0.55m;
                        break;
                    case AccountTypes.Simple:
                        interest.Rate = 1.90m;
                        break;
                    default:
                        throw new Exception($"Interest type unknown: {account.AccountType}");
                }
            }

            return interest;
        }
    }
}
