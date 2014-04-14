using System.Data.Entity;

namespace data.models.write
{
    public class Security
    {
        #region Statics

        public static Security Missing { get; private set; }

        static Security()
        {
            Missing = new Security { SpecId = 0 };
        }

        #endregion

        public int Id { get; set; }
        public int? SpecId { get; set; }
        public DbSet<SecurityDescription> SecurityDescriptions { get; set; }
    }
}