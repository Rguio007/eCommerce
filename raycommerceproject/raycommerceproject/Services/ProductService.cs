using raycommerceproject.Data;
using raycommerceproject.Models;

namespace raycommerceproject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _dbContext;

        public ProductService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProducts()
        {
            var products = _dbContext.Products.ToList();
            return products ?? new List<Product>();
        }


        public Product GetProduct(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public Product AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }

        public Product UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = _dbContext.Products.Find(id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.ImageUrl = updatedProduct.ImageUrl;
            existingProduct.Price = updatedProduct.Price;

            _dbContext.SaveChanges();

            return existingProduct;
        }

        public bool DeleteProduct(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return false;
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return true;
        }

    }

}
