using AbsentSystem.Data.Entities;
using AbsentSystem.Data.Repositories.Contract;

namespace AbsentSystem.Data.Repositories.Implementation
{
    public class VacationRepository : GenericRepositories<TypeVacation>, IVacationRepository
    {
        public VacationRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
