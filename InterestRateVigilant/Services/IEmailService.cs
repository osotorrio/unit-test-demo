using System;

namespace InterestRateVigilant
{
    public interface IEmailService
    {
        void SendEmailToBroker(Guid brokerId, Guid acccountId, decimal rate);
    }

    public class HotmailEmailService : IEmailService
    {
        public void SendEmailToBroker(Guid brokerId, Guid accountId, decimal rate)
        {
            throw new Exception("It could not connect to the email server");
        }
    }
}