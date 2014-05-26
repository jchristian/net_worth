using System.Linq;
using data.models.contexts;
using data.models.write;

namespace core.services
{
    public class AccountService
    {
        DataContext context;

        protected AccountService() { }
        public AccountService(DataContext context)
        {
            this.context = context;
        }

        public virtual Account Find(string account_number)
        {
            return context.Accounts.SingleOrDefault(x => x.Number == account_number);
        }

        public virtual Account Create(string account_number)
        {
            var account = context.Accounts.Add(new Account { Number = account_number });
            context.SaveChanges();
            return context.Accounts.Find(account.Id);
        }
    }
}