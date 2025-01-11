using System.Net.Http;
using System.Text;
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

        public async Task<IActionResult> Create()
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
            bpkb.CreatedBy = User.Identity.Name;

            var client = _httpClientFactory.CreateClient();
            var json = System.Text.Json.JsonSerializer.Serialize(bpkb);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_configuration["ApiSettings:SaveBPKBUrl"], content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "BPKB created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while creating the BPKB.";
            }

            return RedirectToAction("Create", "BPKB", new { username = bpkb.CreatedBy });
        }

    }
}
