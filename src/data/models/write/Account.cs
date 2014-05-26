namespace data.models.write
{
    public class Account
    {
        public int Id { get; set; }
        public int? SpecId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
    }
}