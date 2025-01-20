using BPKB.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace BPKB.Controllers
{
    public class BPKBDashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public BPKBDashboardController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10, string searchTerm = "")
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"{_configuration["ApiSettings:BPKBUrl"]}?pageNumber={pageNumber}&pageSize={pageSize}";
            if (!string.IsNullOrEmpty(searchTerm))
            {
                apiUrl = $"{_configuration["ApiSettings:BPKBUrl"]}{searchTerm}?pageNumber={pageNumber}&pageSize={pageSize}";
            }
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<BPKBDashboardModel>();
                var model = new BPKBDashboardModel
                {
                    BPKBs = result.BPKBs,
                    CurrentPage = result.CurrentPage,
                    TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize),
                    PageSize = result.PageSize,
                    TotalCount = result.TotalCount
                };
                return View("~/Views/BPKB/BPKBDashboard.cshtml", model);
            }
            return View("Error");
        }

        public async Task<IActionResult> Create()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Identity.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("BPKB", "BPKB", new { username = User.Identity.Name });
        }

        public async Task<IActionResult> Update(string agreementNumber = "")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.Identity.Name)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("BPKB", "BPKB", new { AgreementNumber = agreementNumber, username = User.Identity.Name });
        }

        public async Task<IActionResult> Delete(string agreementNumber)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_configuration["ApiSettings:BPKBUrl"]}{agreementNumber}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "BPKB deleted successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the BPKB.";
            }

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
