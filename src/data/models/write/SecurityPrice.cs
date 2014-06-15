using System;

namespace data.models.write
{
    public class SecurityPrice
    {
        public Security Security { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
    }
}