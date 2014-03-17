using data.models.contexts;
using data.models.write;

namespace core.services
{
    public class AccountService
    {
        DataContext context;

        protected AccountService() {}
        public AccountService(DataContext context)
        {
            this.context = context;
        }

        public virtual Account Find(string account_number)
        {
            return context.Accounts.Find(account_number);
        }
    }
}