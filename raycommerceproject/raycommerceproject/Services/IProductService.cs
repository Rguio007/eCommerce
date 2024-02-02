using raycommerceproject.Models;

namespace raycommerceproject.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(int id, Product updatedProduct);
        bool DeleteProduct(int id);
    }
}
