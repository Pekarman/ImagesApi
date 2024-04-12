using Common.Models;

namespace ImagesApi.Interfaces
{
    public interface IAssetService
    {
        public Task<List<Asset>> GetAllAssets();

        public Task<Asset> AddAssetAsync(Asset asset);
    }
}
