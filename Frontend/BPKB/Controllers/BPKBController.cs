using System.Net.Http;
using System.Text;
using System.Text.Json;
using BPKB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace BPKB.Controllers
{
    [Authorize]
    public class BPKBController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BPKBController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BPKB()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(_configuration["ApiSettings:GetLocationUrl"]);
            var locations = JsonConvert.DeserializeObject<List<LocationModel>>(response);

            ViewBag.Locations = new SelectList(locations, "LocationID", "LocationName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BPKBModel bpkb)
        {
            if (string.IsNullOrEmpty(bpkb.AgreementNumber))
            {
                return BadRequest("Agreement number is required.");
            }
            else
            {
                var client = _httpClientFactory.CreateClient();
                var checkResponse = await client.GetAsync($"{_configuration["ApiSettings:BPKBUrl"]}{bpkb.AgreementNumber}");

                if (checkResponse.IsSuccessStatusCode)
                {
                    bpkb.LastUpdateBy = User.Identity.Name;
                    var json = System.Text.Json.JsonSerializer.Serialize(bpkb);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var putResponse = await client.PutAsync(_configuration["ApiSettings:BPKBUrl"], content);

                    if (putResponse.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "BPKB updated successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "An error occurred while updating the BPKB.";
                    }
                }
                else
                {
                    bpkb.CreatedBy = User.Identity.Name;
                    var json = System.Text.Json.JsonSerializer.Serialize(bpkb);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var postResponse = await client.PostAsync(_configuration["ApiSettings:BPKBUrl"], content);

                    if (postResponse.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "BPKB created successfully!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "An error occurred while creating the BPKB.";
                    }
                }
            }

            return RedirectToAction("BPKB", "BPKB", new { username = bpkb.CreatedBy });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string agreementNumber)
        {
            if (string.IsNullOrEmpty(agreementNumber))
            {
                return BadRequest("Agreement number is required.");
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_configuration["ApiSettings:BPKBUrl"]}{agreementNumber}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var bpkb = System.Text.Json.JsonSerializer.Deserialize<BPKBModel>(jsonResponse, options);
                return Json(bpkb);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string agreementNumber)
        {
            if (string.IsNullOrEmpty(agreementNumber))
            {
                return BadRequest("Agreement number is required.");
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_configuration["ApiSettings:BPKBUrl"]}{agreementNumber}");

            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
