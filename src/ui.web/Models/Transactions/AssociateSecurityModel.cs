using System.Collections.Generic;
using data.models.write;

namespace ui.web.Models.Transactions
{
    public class AssociateSecurityModel
    {
        public BrokerageTransaction Transaction { get; set; }
        public IEnumerable<Security> Securities { get; set; }
    }
}