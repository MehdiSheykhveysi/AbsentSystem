using System;
using System.ComponentModel.DataAnnotations;

namespace AbsentSystem.Models.UserViewModel
{
    public class UserEntranceVm
    {
        public int Id { get; set; }
        [Display(Name ="شماره پرسنلی")]
        [Required(ErrorMessage ="واردکردن {0} اجباری است")]
        public string PersonalId { get; set; }

        [Display(Name = "زمان ورود")]
        [Required(ErrorMessage = "واردکردن {0} اجباری است")]
        public string EntranceTime { get; set; }

        [Display(Name = "تاریخ ورود")]
        [Required(ErrorMessage = "واردکردن {0} اجباری است")]
        public string EntranceDate { get; set; }

        public DateTime EntranceDateAD { get; set; }

    }
}