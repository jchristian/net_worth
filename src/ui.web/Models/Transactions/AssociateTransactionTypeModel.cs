using System.Collections.Generic;
using data.models.write;

namespace ui.web.Models.Transactions
{
    public class AssociateTransactionTypeModel
    {
        public int TransactionId { get; set; }
        public BrokerageTransaction Transaction { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
        public TransactionType SelectedTransactionType { get; set; }
    }
}