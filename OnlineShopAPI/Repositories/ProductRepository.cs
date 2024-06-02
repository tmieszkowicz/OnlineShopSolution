using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Data;
using OnlineShopAPI.Entities;
using OnlineShopAPI.Repositories.Contracts;

namespace OnlineShopAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContext;

        public ProductRepository(OnlineShopDbContext onlineShopDbContext)
        {
            this.onlineShopDbContext = onlineShopDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.onlineShopDbContext.ProductCategories.ToListAsync();
            return categories;
        }

        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.onlineShopDbContext.Products.ToListAsync();
            return products;
        }
    }
}
