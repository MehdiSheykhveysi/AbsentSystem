using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AbsentSystem.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbsentSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "وارد کردن ایمیل الزامی است")]
            [EmailAddress]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

            [Required(ErrorMessage = "وارد کردن این فیلد اجباری است")]
            [StringLength(100, ErrorMessage = "طول پسوورد وارد شده مجاز نیست حداکثز 100 گاراکتر و حدااقل 4", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "تکرار رمز عبور")]
            [Compare("Password", ErrorMessage = "پسوورد و تکرار ان مطابقت ندارند")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string code, string Email)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code,
                    Email = Email
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
