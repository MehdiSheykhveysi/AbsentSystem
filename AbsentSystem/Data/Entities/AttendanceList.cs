using System;
using System.Collections.Generic;

namespace AbsentSystem.Data.Entities
{
    public class AttendanceList: BaseEntity<int>
    {
        public DateTime? EntranceTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? EntranceDate { get; set; }
        public DateTime? DepartureDate { get; set; }

        //Navigation Property
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<TypeVacation> Vacations { get; set; }
    }
}
