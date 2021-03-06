﻿using System;
using System.Collections.Generic;

namespace data.models.write
{
    public class BrokerageTransaction
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public int AccountId { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime ProcessDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public string TransactionDescription { get; set; }
        public ICollection<Trade> Trades { get; set; }
        public Security Security { get; set; }
        public int? SecurityId { get; set; }
        public string SecurityDescription { get; set; }
        public decimal SharePrice { get; set; }
        public decimal Shares { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}