namespace LocalFood.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LocalFood.Web.ViewModels.Producers;

    public interface IRegionsService
    {
        IEnumerable<RegionInputModel> GetRegionNamesByCountry(int countryId);
    }
}
