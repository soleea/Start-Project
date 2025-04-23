using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using MiniEcommerceWeb.Models;
using Newtonsoft.Json;
using System.Text;

namespace MiniEcommerceWeb.Controllers
{

    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        string apiBaseUrl = "";
        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

           var  apiBaseUrl = _configuration.GetValue<string>("EcommerceAPIBaseUrl");
        }

        //redirection Page
        [HttpGet]
        public ActionResult MainPage()
        {
            LoginViewModel user = JsonConvert.DeserializeObject<LoginViewModel>(Convert.ToString(TempData["cc"]));
            return View(user);
        }
       
     
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Longin(LoginViewModel user)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/Login";
                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["MainPage"] = JsonConvert.SerializeObject(user);
                        return RedirectToAction("MainPage", "Order");
                    }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                        return View();
                    }
                }
            }
        }


        public async Task<IActionResult> Logout()
        {           
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
