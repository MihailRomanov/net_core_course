namespace EFCoreSamples.Model
{
    public class Category
    {
        public Category()
        {
            Products = [];
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
