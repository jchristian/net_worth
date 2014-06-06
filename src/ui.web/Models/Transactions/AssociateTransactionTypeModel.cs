using System.Collections.Generic;
using data.models.write;

namespace ui.web.Models.Transactions
{
    public class AssociateTransactionTypeModel
    {
        public BrokerageTransaction Transaction { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
    }
}