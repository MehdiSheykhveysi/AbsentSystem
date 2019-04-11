using System;
using System.ComponentModel.DataAnnotations;

namespace AbsentSystem.Models.VacationViewModel
{
    public class VacationIndexVm
    {
        public int Id { get; set; }

        [Display(Name ="عنوان مرخصی")]
        public string Title { get; set; }

        [Display(Name = "مدت زمان مرخصی")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime TimeOff { get; set; }
        public string ShowTimeOff { get; set; }

        [Display(Name = "تاریخ خروج ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MMM-dd}")]
        public DateTime DepartureDate { get; set; }
        public string ShowDepartureDate { get; set; }

        [Display(Name = "ساعت خروج ")]
        public string DepartureTime { get; set; }

        [Display(Name = "تاریخ بازگشت ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MMM-dd}")]
        public DateTime EntranceDate { get; set; }
        public string ShowEntranceDate { get; set; }

        [Display(Name = "ساعت بازگشت ")]
        public string EntranceTime { get; set; }
    }
}
