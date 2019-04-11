using System.ComponentModel.DataAnnotations;

namespace AbsentSystem.Models.UserViewModel
{
    public class UserRegidterDepartureDate
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "زمان ورود")]
        [Required(ErrorMessage = "واردکردن {0} اجباری است")]
        public string EntranceTime { get; set; }

        [Display(Name = "تاریخ ورود")]
        [Required(ErrorMessage = "واردکردن {0} اجباری است")]
        public string EntranceDate { get; set; }

        [Display(Name = "تاریخ بازگشت")]
        [Required(ErrorMessage = "واردکردن {0} اجباری است")]
        public string DepartureDate { get; set; }

        [Display(Name = "ساعت بازگشت")]
        [Required(ErrorMessage = "واردکردن {0} اجباری است")]
        public string DepartureTime { get; set; }
    }
}
