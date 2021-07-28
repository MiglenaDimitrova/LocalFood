namespace LocalFood.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPrevoiusPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PageCount;

        public int PageCount => (int)Math.Ceiling((double)this.ProductsCount / this.ItemsPerPage);

        public int PrevoiusPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int ProductsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}