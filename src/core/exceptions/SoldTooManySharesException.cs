using System;
using data.models.write;

namespace core.exceptions
{
    public class SoldTooManySharesException : Exception
    {
        BrokerageTransaction transaction;

        public SoldTooManySharesException(BrokerageTransaction transaction)
            : base(string.Format("Sold too many shares of <{0}>, Transaction <{1}>", transaction.SecurityId, transaction.Id))
        {
            this.transaction = transaction;
        }
    }
}