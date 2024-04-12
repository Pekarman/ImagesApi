using ImagesApi.Interfaces;
using ImagesApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImagesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService assetService;

        public AssetController(
            IAssetService assetService)
        {
            this.assetService = assetService;
        }

        /// <summary>
        /// Gets all assets.
        /// </summary>
        /// <returns>All assets.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAssetsAsync()
        {
            try
            {
                List<Asset> assets = await this.assetService.GetAllAssets();
                if (assets.Count > 0)
                {
                    return this.Ok(assets);
                }
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult("Assets not found.");
        }


        /// <summary>
        /// Add new asset.
        /// </summary>
        /// <param name="asset">Asset to create.</param>
        /// <returns>Created asset.</returns>
        [HttpPut]
        public async Task<IActionResult> AddAssetAsync(Asset asset)
        {
            try
            {
                Asset result = await this.assetService.AddAssetAsync(asset);
                if (result != null)
                {
                    return this.Ok(asset);
                }
            }
            catch (ArgumentException e)
            {
                return new JsonResult(e.Message);
            }
            catch (Exception e)
            {
                return new JsonResult(e.Message);
            }

            return new JsonResult($"Asset cannot be added.");
        }
    }
}
