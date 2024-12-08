namespace NHibernateSamples.Model
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        // Supplier
        public virtual Category Category { get; set; }
        public virtual string QuantityPerUnit { get; set; }
        public virtual decimal? UnitPrice { get; set; }
        public virtual int? UnitsInStock { get; set; }
        public virtual int? UnitsOnOrder { get; set; }
        public virtual int? ReorderLevel { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}
