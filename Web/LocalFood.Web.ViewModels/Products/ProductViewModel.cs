namespace LocalFood.Web.ViewModels.Products
{
    using AutoMapper;
    using LocalFood.Data.Models;
    using LocalFood.Services.Mapping;

    public class ProductViewModel // : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool IsBio { get; set; } = false;

        public string ProducerName { get; set; }

        public int ProducerId { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

    }
}
