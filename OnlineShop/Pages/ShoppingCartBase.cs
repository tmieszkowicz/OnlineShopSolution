using Microsoft.AspNetCore.Components;
using OnlineShopModels.Dtos;
using OnlineShopWeb.Services;
using OnlineShopWeb.Services.Contracts;

namespace OnlineShopWeb.Pages
{
	public class ShoppingCartBase : ComponentBase
	{
		[Inject]
		public IShoppingCartService ShoppingCartService { get; set; }
		public IEnumerable<CartItemDto> ShoppingCartItems { get; set; }
		public string ErrorMessage {  get; set; }
		protected override async Task OnInitializedAsync()
		{
			try
			{
				ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
			}
			catch (Exception ex)
			{

				ErrorMessage = ex.Message;
			}
		}

	}
}
