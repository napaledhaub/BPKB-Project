using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BPKB.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection;

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

        public async Task<IActionResult> BPKB(string agreementNumber = "")
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(_configuration["ApiSettings:GetLocationUrl"]);
            var locations = JsonConvert.DeserializeObject<List<LocationModel>>(response);

            ViewBag.Locations = new SelectList(locations, "LocationID", "LocationName");
            var model = new BPKBModel();
            if (!string.IsNullOrEmpty(agreementNumber))
            {
                var bpkb = await Search(agreementNumber);
                ViewBag.AgreementNumber = bpkb.AgreementNumber;
                ViewBag.BranchID = bpkb.BranchID;
                ViewBag.BPKBNo = bpkb.BPKBNo;
                ViewBag.BPKBDateIn = bpkb.BPKBDateIn?.ToString("yyyy-MM-dd");
                ViewBag.BPKBDate = bpkb.BPKBDate?.ToString("yyyy-MM-dd");
                ViewBag.FakturNo = bpkb.FakturNo;
                ViewBag.FakturDate = bpkb.FakturDate?.ToString("yyyy-MM-dd");
                ViewBag.PoliceNo = bpkb.PoliceNo;
                ViewBag.CreatedBy = bpkb.CreatedBy;
                ViewBag.LastUpdateBy = bpkb.LastUpdateBy;
                ViewBag.LastUpdateOn = bpkb.LastUpdateOn?.ToString("yyyy-MM-dd");
                ViewBag.CreatedOn = bpkb.CreatedOn?.ToString("yyyy-MM-dd");
                model.LocationID = bpkb.LocationID;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<BPKBModel> Search(string agreementNumber)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"{_configuration["ApiSettings:BPKBUrl"]}{agreementNumber}?pageNumber={1}&pageSize={10}";
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var bpkbs = await response.Content.ReadFromJsonAsync<BPKBDashboardModel>();
                var bpkb = bpkbs.BPKBs.ToList().FirstOrDefault();
                return bpkb;
            }
            else
            {
                return new BPKBModel();
            }
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
                var apiUrl = $"{_configuration["ApiSettings:BPKBUrl"]}{bpkb.AgreementNumber}?pageNumber={1}&pageSize={10}";
                var checkResponse = await client.GetAsync(apiUrl);

                if (checkResponse.IsSuccessStatusCode)
                {
                    var result = await checkResponse.Content.ReadFromJsonAsync<BPKBDashboardModel>();

                    if (result.TotalCount == 0)
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
                    else
                    {
                        bpkb.LastUpdateBy = User.Identity.Name;
                        var json = System.Text.Json.JsonSerializer.Serialize(bpkb);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var postResponse = await client.PutAsync(_configuration["ApiSettings:BPKBUrl"], content);

                        if (postResponse.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "BPKB updated successfully!";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "An error occurred while creating the BPKB.";
                        }
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "An error occurred while creating the BPKB.";
                }
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Identity.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "BPKBDashboard", new { pageNumber = 1, pageSize = 10, username = User.Identity.Name });
        }

        public async Task<IActionResult> Cancel()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Identity.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "BPKBDashboard", new { pageNumber = 1, pageSize = 10, username = User.Identity.Name });
        }
    }
}
