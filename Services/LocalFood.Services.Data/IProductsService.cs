﻿namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using LocalFood.Data.Models;
    using LocalFood.Web.ViewModels.Products;

    public interface IProductsService
    {
        Task AddProduct(ProductInputModel input, string userId);

        ICollection<CategoryInputModel> GetCategories();
    }
}