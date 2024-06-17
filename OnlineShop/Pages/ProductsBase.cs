using Microsoft.AspNetCore.Components;
using OnlineShopModels.Dtos;
using OnlineShopWeb.Services;
using OnlineShopWeb.Services.Contracts;

namespace OnlineShopWeb.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService {  get; set; }
        public IShoppingCartService ShoppingCartService {  get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
        public string ErrorMessage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Products = await ProductService.GetItems();

                var shoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                var totalQty = shoppingCartItems.Sum(i=>i.Qty);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
			}
            
        }
        protected IOrderedEnumerable<IGrouping<int,ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in Products
				   group product by product.CategoryId into prodByCatGroup
				   orderby prodByCatGroup.Key
				   select prodByCatGroup;
		}
        protected string GetCategoryName(IGrouping<int,ProductDto> groupedProductDtos)
        {
            return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key).CategoryName;
        }
    }
}
