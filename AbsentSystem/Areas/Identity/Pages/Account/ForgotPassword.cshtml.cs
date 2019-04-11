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
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ForgotPasswordModel(UserManager<User> userManager)
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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed

                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var callbackUrl = Url.Page(
                //    "/Account/ResetPassword",
                //    pageHandler: null,
                //    values: new { code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    Input.Email,
                //    "Reset Password",
                //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                var model = new
                {
                    code,
                    user.Email
                };
                return RedirectToPage("./ResetPassword", model);
            }

            return Page();
        }
    }
}
