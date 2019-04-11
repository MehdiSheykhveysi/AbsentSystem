using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AbsentSystem.Models;
using Microsoft.AspNetCore.Identity;
using AbsentSystem.Data.Entities;
using System.Threading.Tasks;
using AbsentSystem.Models.UserViewModel;

namespace AbsentSystem.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<User> _userManager { get; set; }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public HomeController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            await this.CreateAdmin();
            return View();
        }

        public async Task CreateAdmin()
        {
            UserAdminInfo adminInfo = new UserAdminInfo();
            if (await _userManager.FindByEmailAsync("a.Admin1234@gmail.com") != null)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                User user = new User
                {
                    Email = "a.Admin1234@gmail.com",
                    UserName = "a.Admin1234@gmail.com",
                    DisplayName = "RootAdmin",
                    Address = "Iran-Tehran",
                    PhoneNumber = "09991234567",
                    PersonelId = "123455667",
                    NationalCode = "1234567891",
                    ShowPass = "Admin1234",
                };
                await _roleManager.CreateAsync(role);
                var cuser = await _userManager.CreateAsync(user, user.ShowPass);
                await _userManager.AddToRoleAsync(user, "Admin");

                adminInfo.Email = user.Email;
                adminInfo.Password = user.ShowPass;

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
