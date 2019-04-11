using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AbsentSystem.Data.Entities
{
    public class User : IdentityUser, IEntiity
    {
        public User()
        {

        }
        public string DisplayName { get; set; }
        public string PersonelId { get; set; }
        public string ShowPass { get; set; }
        public string Address { get; set; }
        public string NationalCode { get; set; }

        //Navigation Property
        public ICollection<AttendanceList> AttendanceLists { get; set; }
    }
}