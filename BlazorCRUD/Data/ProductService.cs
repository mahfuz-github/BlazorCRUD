using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Data
{
    public class ProductService
    {
        private readonly DataContext _context;
        private readonly NavigationManager _navigationManager;

        public ProductService(DataContext context
            , NavigationManager navigationManager)
        {
            _context = context;
            _navigationManager = navigationManager;
            _context.Database.EnsureCreated();
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            _navigationManager.NavigateTo("products");
        }

        public async Task DeleteProduct(int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null)
                throw new Exception("No product here. :/");

            _context.Products.Remove(dbProduct);
            await _context.SaveChangesAsync();
            _navigationManager.NavigateTo("products");
        }

        public async Task<Product> GetSingleProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new Exception("No product here. :/");
            return product;
        }

        public async Task LoadProducts()
        {
            Products = await _context.Products.ToListAsync();
        }

        public async Task UpdateProduct(Product product, int id)
        {
            var dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null)
                throw new Exception("No product here. :/");

            dbProduct.Name = product.Name;
           dbProduct.Description = product.Description;
            dbProduct.Price = product.Price;

            await _context.SaveChangesAsync();
            _navigationManager.NavigateTo("products");
        }
    }
}
