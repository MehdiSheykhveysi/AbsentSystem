namespace AbsentSystem.Data.Entities
{
    public class TypeVacation:BaseEntity<int>
    {
        public string Title { get; set; }

        //navigation Property
        public int AttendanceListId { get; set; }
        public AttendanceList AttendanceList { get; set; }
    }
}
