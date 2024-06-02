using Microsoft.AspNetCore.Components;
using OnlineShopModels.Dtos;

namespace OnlineShopWeb.Pages
{
	public class DisplayProductsBase : ComponentBase
	{
		[Parameter]
		public IEnumerable<ProductDto> Products { get; set; }
	}
}
