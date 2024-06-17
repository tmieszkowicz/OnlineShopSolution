﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OnlineShopModels.Dtos;
using OnlineShopWeb.Services;
using OnlineShopWeb.Services.Contracts;

namespace OnlineShopWeb.Pages
{
	public class ShoppingCartBase : ComponentBase
	{
		[Inject]
		public IShoppingCartService ShoppingCartService { get; set; }
		public List<CartItemDto> ShoppingCartItems { get; set; }
		public string ErrorMessage {  get; set; }
		protected String TotalPrice { get; set; }
		protected int TotalQuantity {  get; set; }
		[Inject]
		public IJSRuntime Js { get; set; }
		protected override async Task OnInitializedAsync()
		{
			try
			{
				ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
				CartChanged();
			}
			catch (Exception ex)
			{

				ErrorMessage = ex.Message;
			}
		}

		protected async Task DeleteCartItem_Click(int id)
		{
			var cartItemDto = await ShoppingCartService.DeleteItem(id);

			RemoveCartItem(id);
			CartChanged();
		}

		private CartItemDto GetCartItem(int id)
		{
			return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
		}

		private void RemoveCartItem(int id)
		{
			var cartItemDto = GetCartItem(id);

			ShoppingCartItems.Remove(cartItemDto);
		}

		private void CartChanged()
		{
			CalculateCartSummaryTotals();

			ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
		}

		protected async Task UpdateQtyCartItem_Click(int id, int qty)
		{
			try
			{
				if (qty > 0)
				{
					var updateItemDto = new CartItemQtyUpdateDto
					{
						CartItemId = id,
						Qty = qty
					};

					var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQty(updateItemDto);

					UpdateItemTotalPrice(returnedUpdateItemDto);
					CartChanged();

					await MakeUpdateQtyButtonVisible(id, false);
				}
				else
				{
					var item = this.ShoppingCartItems.FirstOrDefault(i=>i.Id == id);
					if (item != null) 
					{
						item.Qty = 1;
						item.TotalPrice = item.Price;
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void SetTotalPrice()
		{
			TotalPrice = this.ShoppingCartItems.Sum(i => i.TotalPrice).ToString("C");
		}
		private void SetTotalQuantity()
		{
			TotalQuantity = this.ShoppingCartItems.Sum(i => i.Qty);	
		}
		private void CalculateCartSummaryTotals()
		{
			SetTotalPrice();
			SetTotalQuantity();
		}
		private void UpdateItemTotalPrice(CartItemDto cartItemDto)
		{
			var item = GetCartItem(cartItemDto.Id);
			if (item != null)
			{
				item.TotalPrice = cartItemDto.TotalPrice * cartItemDto.Qty;
			} 
		}
		protected async Task UpdateQty_Input(int id)
		{
			await MakeUpdateQtyButtonVisible(id, true);
		}

		private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
		{
			await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
		}
	}
}
