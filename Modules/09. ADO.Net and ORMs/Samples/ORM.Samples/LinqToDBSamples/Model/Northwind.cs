using LinqToDB;
using LinqToDB.Data;

namespace LinqToDBSamples.Model
{
    public class Northwind : DataConnection
    {
        public Northwind(DataOptions dataOptions) : base(dataOptions) { }

        public ITable<Category> Categories => this.GetTable<Category>();
        public ITable<Product> Products => this.GetTable<Product>();
    }
}
