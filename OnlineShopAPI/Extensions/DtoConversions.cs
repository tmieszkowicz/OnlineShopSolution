﻿using System.Collections;
using OnlineShopAPI.Entities;
using OnlineShopModels.Dtos;

namespace OnlineShopAPI.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products,
                                                            IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join ProductCategory in productCategories
                    on product.CategoryId equals ProductCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                        CategoryName = ProductCategory.Name
                    }).ToList();
        }
		public static ProductDto ConvertToDto(this Product product,
													ProductCategory productCategory)
		{
            return new ProductDto 
            { 
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };
		}
	}
}
