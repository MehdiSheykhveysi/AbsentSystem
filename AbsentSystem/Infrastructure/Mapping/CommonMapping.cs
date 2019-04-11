using AbsentSystem.Data.Entities;
using AbsentSystem.Models.UserViewModel;
using AbsentSystem.Models.VacationViewModel;
using AutoMapper;
using System.Collections.Generic;

namespace AbsentSystem.Infrastructure.Mapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<List<AttendaceListModel>, List<AttendanceList>>();
            CreateMap<UserEntranceVm, User>().ReverseMap();
            CreateMap<List<VacationIndexVm>, List<TypeVacation>>();
            CreateMap<TypeVacation, VacationIndexVm>().ReverseMap();
            CreateMap<UserListVm, List<User>>();
        }
    }
}