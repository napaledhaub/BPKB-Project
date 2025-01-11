using BPKBAPI.Models;
using BPKBAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BPKBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageLocationsController : ControllerBase
    {
        private readonly IStorageLocationService _service;

        public StorageLocationsController(IStorageLocationService service)
        {
            _service = service;
        }

        [HttpGet("GetStorageLocations")]
        public async Task<ActionResult<IEnumerable<StorageLocation>>> GetStorageLocations()
        {
            var locations = await _service.GetAllStorageLocationsAsync();
            return Ok(locations);
        }
    }
}
