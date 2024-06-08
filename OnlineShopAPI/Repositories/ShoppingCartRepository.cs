using Microsoft.EntityFrameworkCore;
using OnlineShopAPI.Data;
using OnlineShopAPI.Entities;
using OnlineShopAPI.Repositories.Contracts;
using OnlineShopModels.Dtos;

namespace OnlineShopAPI.Repositories
{
	public class ShoppingCartRepository : IShoppingCartRepository
	{
		private readonly OnlineShopDbContext onlineShopDbContext;

		public ShoppingCartRepository(OnlineShopDbContext onlineShopDbContext)
		{
			this.onlineShopDbContext = onlineShopDbContext;
		}
		
		private async Task<bool> CartItemExists(int cartId, int productId)
		{
			return await this.onlineShopDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
		}

		public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
		{
			if(await CartItemExists(cartItemToAddDto.CartId,cartItemToAddDto.ProductId) == false)
			{
				var item = await (from product in this.onlineShopDbContext.Products
								  where product.Id == cartItemToAddDto.ProductId
								  select new CartItem
								  {
									  CartId = cartItemToAddDto.CartId,
									  ProductId = product.Id,
									  Qty = cartItemToAddDto.Qty,
								  }).SingleOrDefaultAsync();

				if (item != null)
				{
					var result = await this.onlineShopDbContext.AddAsync(item);
					await this.onlineShopDbContext.SaveChangesAsync();
					return result.Entity;
				}
			}

			return null;
		}

		public async Task<CartItem> DeleteItem(int id)
		{
			var item = await this.onlineShopDbContext.CartItems.FindAsync(id);

			if (item != null)
			{
				this.onlineShopDbContext.CartItems.Remove(item); 
				await this.onlineShopDbContext.SaveChangesAsync();
			}

			return item;
		}

		public async Task<CartItem> GetItem(int id)
		{

			return await (from cart in this.onlineShopDbContext.Carts
						  join cartItem in this.onlineShopDbContext.CartItems
						  on cart.Id equals cartItem.CartId
						  where cartItem.Id == id
						  select new CartItem
						  {
							  Id = cartItem.Id,
							  ProductId = cartItem.ProductId,
							  Qty = cartItem.Qty,
							  CartId = cartItem.CartId
						  }).SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<CartItem>> GetItems(int userId)
		{
			return await (from cart in this.onlineShopDbContext.Carts
						  join cartItem in this.onlineShopDbContext.CartItems
						  on cart.Id equals cartItem.CartId
						  where cart.UserId == userId
						  select new CartItem
						  {
							Id = cartItem.Id,
							ProductId = cartItem.ProductId,
							Qty	= cartItem.Qty,
							CartId = cartItem.CartId
						  }).ToListAsync();
		}

		public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
		{
			throw new NotImplementedException();
		}
	}
}
