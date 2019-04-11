using AbsentSystem.Data.Entities;
using AbsentSystem.Data.Repositories.Contract;
using AbsentSystem.Infrastructure.Utilities;
using AbsentSystem.Models.VacationViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AbsentSystem.Controllers
{
    public class VacationController : Controller
    {
        private readonly IVacationRepository _vacationRepository;
        private readonly IMapper _mapper;

        public VacationController(IVacationRepository vacationRepository, IMapper mapper)
        {
            this._vacationRepository = vacationRepository;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken, string Id = null)
        {
            List<TypeVacation> vacations = await _vacationRepository.Entities.Where(v => string.IsNullOrEmpty(Id) || v.AttendanceListId.ToString().Equals(Id)).ToListAsync(cancellationToken);
            ViewData["CurrentDate"] = string.IsNullOrEmpty(Id) ? "کل روز ها" : vacations.First().DepartureDate.ToPersionDate();
            List<VacationIndexVm> model = _mapper.Map<List<VacationIndexVm>>(vacations);
            return View(model);
        }

        public async Task<IActionResult> ReturnFromVacation(int Id, CancellationToken cancellationToken)
        {
            if (Id == 0) return NotFound();
            TypeVacation vacation = await _vacationRepository.Entities.FirstOrDefaultAsync(c => c.Id == Id, cancellationToken);
            VacationIndexVm model = new VacationIndexVm
            {
                TimeOff = vacation.TimeOff,
                Title = vacation.Title,
                DepartureDate = vacation.DepartureDate,
                DepartureTime = vacation.DepartureTime,
                EntranceDate = DateTime.Now,
                EntranceTime = DateTime.Now.ToString("HH:mm"),
                ShowDepartureDate = vacation.DepartureDate.ToPersionDate(),
                ShowEntranceDate = DateTime.Now.ToPersionDate(),
                ShowTimeOff = vacation.TimeOff.ToString("HH:mm")
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReturnFromVacation(VacationIndexVm model, CancellationToken cancellationToken)
        {
            TypeVacation vacation = await _vacationRepository.Entities.FirstOrDefaultAsync(c => c.Id == model.Id, cancellationToken);
            vacation.EntranceDate = DateTime.Now;
            vacation.EntranceTime = DateTime.Now.ToString("HH:mm");
            await _vacationRepository.UpdateAsync(vacation, cancellationToken);
            return Redirect("/User/Index");
        }
    }
}