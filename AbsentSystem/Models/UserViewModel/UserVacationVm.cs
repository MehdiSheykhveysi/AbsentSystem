using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AbsentSystem.Models.UserViewModel
{
    public class UserVacationVm
    {
        public int Id { get; set; }
        [Display(Name ="عنوان مرخصی")]
        [Required(ErrorMessage ="پر کردن این فیلد اجباری است")]
        public string Title { get; set; }

        [Display(Name = "مدت زمان مرخصی")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime TimeOff { get; set; }

        [Display(Name = "تاریخ خروح")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
        [ReadOnly(true)]
        public string DepartureDate { get; set; }

        [ReadOnly(true)]
        public DateTime DepartureDateAD { get; set; }

        [Display(Name = "زمان خروج")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری است")]
        [ReadOnly(true)]
        public string DepartureTime { get; set; }
    }
}
