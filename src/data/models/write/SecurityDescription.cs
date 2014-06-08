namespace data.models.write
{
    public class SecurityDescription
    {
        public int Id { get; set; }
        public int SecurityId { get; set; }
        public Security Security { get; set; }
        public string Description { get; set; }
    }
}