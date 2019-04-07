using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AbsentSystem.Data.Entities
{
    public class User : IdentityUser, IEntiity
    {
        public User()
        {

        }
        public string PersonnelId { get; set; }


        //Navigation Property
        public ICollection<AttendanceList> AttendanceLists { get; set; }
    }
}