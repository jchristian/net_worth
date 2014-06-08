namespace data.models.write
{
    public class TransactionMatch
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public TransactionMatchType TransactionMatchType { get; set; }
        public string ContainsMatchString { get; set; }
    }
}