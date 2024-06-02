using Microsoft.AspNetCore.Components;
using OnlineShopModels.Dtos;
using OnlineShopWeb.Services.Contracts;

namespace OnlineShopWeb.Pages
{
	public class ProductDetailsBase : ComponentBase
	{
		[Parameter]
		public int Id { get; set; }
		[Inject]
		public IProductService ProductService { get; set; }
		public ProductDto Product { get; set; }
		public string ErrorMessage { get; set; }
		protected override async Task OnInitializedAsync()
		{
			try
			{
				Product = await ProductService.GetItem(Id);
			}
			catch (Exception ex)
			{
				ErrorMessage = ex.Message;
				throw;
			}
		}
	}
}
