using Microsoft.EntityFrameworkCore.Infrastructure;

namespace QueryAndChangeDataSamples.Model;

public class CategoryWithLoader
{
    private readonly ILazyLoader? lazyLoader;
    ICollection<Product>? products;

    public CategoryWithLoader() { }
    public CategoryWithLoader(ILazyLoader lazyLoader)
    {
        this.lazyLoader = lazyLoader;
    }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<Product>? Products
    {
        get => lazyLoader?.Load(this, ref products);
        set => products = value;
    }
}
