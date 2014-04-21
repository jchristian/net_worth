using System;
using core.services;
using data.models.write;

namespace core.importers.parsers.mappers
{
    public class ParsedVanguardCVSRowMapper
    {
        SecurityService security_service;
        AccountService account_service;
        TransactionTypeService transaction_type_service;

        protected ParsedVanguardCVSRowMapper() {}
        public ParsedVanguardCVSRowMapper(SecurityService security_service, AccountService account_service, TransactionTypeService transaction_type_service)
        {
            this.security_service = security_service;
            this.account_service = account_service;
            this.transaction_type_service = transaction_type_service;
        }

        public virtual BrokerageTransaction Map(dynamic parsed_row)
        {
            return new BrokerageTransaction
                   {
                       Account = account_service.Find(parsed_row.AccountNumber),
                       TradeDate = DateTime.Parse(parsed_row.TradeDate),
                       ProcessDate = DateTime.Parse(parsed_row.ProcessDate),
                       TransactionType = transaction_type_service.Find(parsed_row.TransactionType),
                       TransactionDescription = parsed_row.TransactionDescription,
                       Security = security_service.Find(parsed_row.InvestmentName),
                       SecurityDescription = parsed_row.InvestmentName,
                       SharePrice = Decimal.Parse(parsed_row.SharePrice),
                       Shares = Decimal.Parse(parsed_row.Shares),
                       GrossAmount = Decimal.Parse(parsed_row.GrossAmount),
                       NetAmount = Decimal.Parse(parsed_row.NetAmount),
                   };
        }
    }
}