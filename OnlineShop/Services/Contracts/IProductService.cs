using OnlineShopModels.Dtos;

namespace OnlineShopWeb.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetItems();
    }
}
