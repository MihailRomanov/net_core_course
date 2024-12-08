using LinqToDB.Mapping;

namespace LinqToDBSamples.Model
{
    [Table("Products", Schema = "Northwind")]
    public class Product
    {
        [Column("ProductID"), Identity, PrimaryKey]
        public int Id { get; set; }
        [Column("ProductName")]
        public string Name { get; set; }
        [Association(ThisKey = "CategoryID", OtherKey = "Id")]
        public Category Category { get; set; }
        [Column]
        public int CategoryID { get; set; }
        [Column]
        public string QuantityPerUnit { get; set; }
        [Column]
        public decimal? UnitPrice { get; set; }
        [Column]
        public int? UnitsInStock { get; set; }
        [Column]
        public int? UnitsOnOrder { get; set; }
        [Column]
        public int? ReorderLevel { get; set; }
        [Column]
        public bool Discontinued { get; set; }
    }
}
