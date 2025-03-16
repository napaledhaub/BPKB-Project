using BPKBAPI.Models;
using BPKBAPI.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BPKBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogBreedController : ControllerBase
    {
        private readonly IDogBreedsService _service;

        public DogBreedController(IDogBreedsService service)
        {
            _service = service;
        }

        [HttpGet("GetDogBreeds")]
        public async Task<IActionResult> GetDogBreeds()
        {
            DogBreedsResponse response = await _service.FetchBreedsAsync();
            if (response.Status != "success")
            {
                return BadRequest(response);
            }
            return Ok(response.Message);
        }
    }
}
