using System;

namespace InterestRateVigilant
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(Guid userId);
    }

    public class MongoCustomerRepository : ICustomerRepository
    {
        public Customer GetCustomerById(Guid userId)
        {
            throw new Exception("The No-SQL database does not exist");
        }
    }
}