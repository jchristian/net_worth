using System;
using core.extensions;
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
                       AccountId = (account_service.Find(parsed_row.AccountNumber) ?? account_service.Create(parsed_row.AccountNumber)).Id,
                       TradeDate = DateTime.Parse(parsed_row.TradeDate),
                       ProcessDate = DateTime.Parse(parsed_row.ProcessDate),
                       TransactionType = transaction_type_service.Find(parsed_row.TransactionType),
                       TransactionDescription = parsed_row.TransactionDescription,
                       SecurityId = security_service.Find((string)parsed_row.InvestmentName),
                       SecurityDescription = parsed_row.InvestmentName,
                       SharePrice = Decimal.Round(Decimal.Parse(parsed_row.SharePrice), 6),
                       Shares = Decimal.Round(Decimal.Parse(parsed_row.Shares), 6),
                       GrossAmount = Decimal.Round(Decimal.Parse(parsed_row.GrossAmount), 6),
                       NetAmount = Decimal.Round(Decimal.Parse(parsed_row.NetAmount), 6),
                   };
        }
    }
}