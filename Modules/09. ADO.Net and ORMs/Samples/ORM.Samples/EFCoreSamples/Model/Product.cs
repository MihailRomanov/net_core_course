using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreSamples.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }
    }
}
