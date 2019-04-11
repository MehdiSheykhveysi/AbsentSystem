using AbsentSystem.Data.Entities;
using AbsentSystem.Data.Repositories.Contract;

namespace AbsentSystem.Data.Repositories.Implementation
{
    public class AttendanceListRepository : GenericRepositories<AttendanceList>, IAttendanceListRepository
    {
        public AttendanceListRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
