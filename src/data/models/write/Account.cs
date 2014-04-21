namespace data.models.write
{
    public class Account
    {
        #region Statics

        public static Account Missing { get; private set; }

        static Account()
        {
            Missing = new Account { SpecId = 0 };
        }

        #endregion

        public int Id { get; set; }
        public int? SpecId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
    }
}