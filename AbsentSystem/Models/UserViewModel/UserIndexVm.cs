using System;
using System.Collections.Generic;

namespace AbsentSystem.Models.UserViewModel
{
    public class UserIndexVm
    {
        public UserIndexVm() => this.AttendaceLists = new List<AttendaceListModel>();
        public List<AttendaceListModel> AttendaceLists;
    }

    public class AttendaceListModel
    {
        public int Id { get; set; }
        public DateTime EntranceTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime EntranceDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
