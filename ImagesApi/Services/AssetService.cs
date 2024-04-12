using ImagesApi.Interfaces;
using ImagesApi.Models;
using Newtonsoft.Json;
using System.Data.Common;

namespace ImagesApi.Services
{
    public class AssetService : IAssetService
    {
        string assetsDirectory = AppContext.BaseDirectory + @"\Assets";

        public async Task<List<Asset>> GetAllAssets()
        {
            List<Asset> assets = new List<Asset>();

            foreach (string fileName in Directory.EnumerateFiles(assetsDirectory))
            {
                var json = File.ReadAllText(fileName);
                var asset = JsonConvert.DeserializeObject<Asset>(json);

                if (asset == null) continue;

                assets.Add(asset);
            }

            return assets;
        }

        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            asset.Id = DateTime.Now.Ticks;

            string fileName = $"{assetsDirectory}\\{asset.Id}_Asset.json";

            var json = JsonConvert.SerializeObject(asset);
            File.WriteAllText(fileName, json);

            return asset;
        }
    }
}
