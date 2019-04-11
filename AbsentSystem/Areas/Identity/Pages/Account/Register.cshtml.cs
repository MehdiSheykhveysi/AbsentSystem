using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AbsentSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace AbsentSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "وارد کردن فیلد ایمیل اجباری است")]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "وارد کردن فیلد  نام کاربری اجباری است")]
            [Display(Name = "نام کاربری")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "وارد کردن فیلد کدملی اجباری است")]
            [StringLength(10, ErrorMessage = "طول کدملی وارد شده مجاز نیست حداکثز 10 گاراکتر و حدااقل 10", MinimumLength = 10)]
            [Display(Name = "کدملی")]
            public string NationalCode { get; set; }

            [Required(ErrorMessage = "وارد کردن فیلد  شماره تلفن اجباری است")]
            [Display(Name = "شماره تلفن")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "وارد کردن فیلد رمز عبور اجباری است")]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [Required(ErrorMessage = "وارد کردن  فیلد آدرس اجباری است")]
            [Display(Name = "آدرس")]
            public string Address { get; set; }


        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? "/";
            if (ModelState.IsValid)
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                User user = new User()
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Address = Input.Address,
                    NationalCode = Input.NationalCode,
                    PhoneNumber = Input.PhoneNumber,
                    ShowPass = Input.Password,
                    PersonelId = rand.Next(1, 10000000).ToString(),
                    DisplayName=Input.UserName
                };

                IdentityResult result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
