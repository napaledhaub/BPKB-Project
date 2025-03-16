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
            DogBreedsResponse response = await _service.FetchBreeds();
            if (response.Status != "success")
            {
                return BadRequest(response);
            }
            return Ok(response.Message);
        }

        [HttpGet("GetDogImages")]
        public async Task<IActionResult> GetDogImages(string breed, int numberOfImages)
        {
            if (numberOfImages <= 0)
            {
                return BadRequest();
            }

            var result = await _service.GetDogImages(breed, numberOfImages);
            if (result.Status != "success")
            {
                return NotFound();
            }

            return Ok(result.Message);
        }
    }
}
