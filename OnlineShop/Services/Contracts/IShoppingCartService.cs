using OnlineShopModels.Dtos;

namespace OnlineShopWeb.Services.Contracts
{
	public interface IShoppingCartService
	{
		Task<IEnumerable<CartItemDto>> GetItems(int userId);
		Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);
	}
}
