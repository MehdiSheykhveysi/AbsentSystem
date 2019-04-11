using System;

namespace AbsentSystem.Data.Entities
{
    public class TypeVacation:BaseEntity<int>
    {
        public string Title { get; set; }
        public DateTime TimeOff { get; set; }
        public DateTime DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public DateTime EntranceDate { get; set; }
        public string EntranceTime { get; set; }
        //navigation Property
        public int? AttendanceListId { get; set; }
        public AttendanceList AttendanceList { get; set; }
    }
}