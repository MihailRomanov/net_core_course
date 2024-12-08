using LinqToDB.Mapping;

namespace LinqToDBSamples.Model
{
    [Table("Categories", Schema = "Northwind")]
    public class Category
    {
        [PrimaryKey]
        [Identity]
        [Column("CategoryID")]
        public int Id { get; set; }
        [Column("CategoryName")]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public byte[] Picture { get; set; }
    }
}
