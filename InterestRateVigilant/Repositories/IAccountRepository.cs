using System;

namespace InterestRateVigilant
{
    public interface IAccountRepository
    {
        Account GetAccountById(Guid accountId);
    }

    public class SqlServerAccountRepository : IAccountRepository
    {
        public Account GetAccountById(Guid accountId)
        {
            throw new Exception("The SQL database does not exist");
        }
    }
}