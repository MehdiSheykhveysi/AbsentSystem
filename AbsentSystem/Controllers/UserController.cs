using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AbsentSystem.Data.Entities;
using AbsentSystem.Data.Repositories.Contract;
using AbsentSystem.Infrastructure.Utilities;
using AbsentSystem.Models.UserViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbsentSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IAttendanceListRepository _attendanceListRepository;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper, IAttendanceListRepository AttendanceListRepository)
        {
            this._userManager = userManager;
            this._mapper = mapper;
            this._attendanceListRepository = AttendanceListRepository;
        }

        public async Task<IActionResult> Index(string Id, CancellationToken cancellationToken)
        {
            User user = await _userManager.Users.Where(u => u.Id.Equals(Id)).Include(u => u.AttendanceLists).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            UserIndexVm model = new UserIndexVm
            {
                AttendaceLists = _mapper.Map<List<AttendaceListModel>>(user.AttendanceLists)
            };
            return View(model);
        }

        public async Task<IActionResult> UsersList(CancellationToken cancellationToken)
        {
            List<User> users = await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken);
            List<UserListVm> list = _mapper.Map<List<UserListVm>>(users);
            return View(list);
        }

        public ViewResult RegistrationAttendance()
        {
            return View("RegistrationAttendance");
        }

        public async Task<IActionResult> EntranceForm(string PersonalId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(PersonalId)) return BadRequest();
            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.PersonelId.Equals(PersonalId), cancellationToken);
            if (user == null) return NotFound();
            @ViewData["SearchKey"] = PersonalId;
            UserEntranceVm model = new UserEntranceVm
            {
                PersonalId = user.PersonelId,
                EntranceDateAD = DateTime.Now,
                EntranceDate = DateTime.Now.ToPersionDate(),
                EntranceTime = DateTime.Now.ToString("HH:mm")
            };

            return PartialView("EntrancePartialView", model);
        }

        public IActionResult Vacation(int Id)
        {
            if (Id == 0)
                return BadRequest();
            UserVacationVm model = new UserVacationVm
            {
                Id = Id,
                DepartureDate = DateTime.Now.ToPersionDate(),
                DepartureTime = DateTime.Now.ToString("HH:mm"),
                DepartureDateAD = DateTime.Now,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vacation(UserVacationVm model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(model);
            AttendanceList enterance = await _attendanceListRepository.Entities.FirstOrDefaultAsync(c => c.Id == model.Id, cancellationToken);

            enterance.Vacations = new List<TypeVacation>
            {
                new TypeVacation
                {
                    AttendanceListId=enterance.Id,
                    TimeOff=model.TimeOff,
                    Title=model.Title,
                    DepartureDate = model.DepartureDateAD,
                    DepartureTime = model.DepartureTime
        }
            };
            await _attendanceListRepository.UpdateAsync(enterance, cancellationToken);
            return RedirectToAction(nameof(Index), new { Id = User.FindFirst(ClaimTypes.NameIdentifier).Value });
        }

        [HttpPost]
        public async Task<IActionResult> EntranceForm(UserEntranceVm model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.Users.Where(u => u.PersonelId.Equals(model.PersonalId)).FirstOrDefaultAsync(cancellationToken);
                user.AttendanceLists = new List<AttendanceList>
                {
                    new AttendanceList
                    {
                        EntranceDate=model.EntranceDateAD,
                        EntranceTime=DateTime.ParseExact(model.EntranceTime, "HH:mm", null, System.Globalization.DateTimeStyles.None)
                    },
                };
                await _userManager.UpdateAsync(user);
            }
            return Redirect("/");
        }

        public async Task<IActionResult> SetRegidterDepartureDate(int Id, CancellationToken cancellationToken)
        {
            if (Id == 0) return NotFound();
            AttendanceList attandance = await _attendanceListRepository.Entities.FirstOrDefaultAsync(c => c.Id == Id, cancellationToken);
            UserRegidterDepartureDate model = new UserRegidterDepartureDate
            {
                Id = attandance.Id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                EntranceDate = attandance.EntranceDate.Value.ToPersionDate(),
                EntranceTime = attandance.EntranceTime.Value.ToString("HH:mm"),
                DepartureDate = DateTime.Now.ToPersionDate(),
                DepartureTime = DateTime.Now.ToString("HH:mm")
            };
            return View("RegidterDepartureDate", model);
        }

        public async Task<IActionResult> RegidterDepartureDate(UserRegidterDepartureDate model, CancellationToken cancellationToken)
        {
            if (model.Id == 0) return NotFound();
            AttendanceList attandance = await _attendanceListRepository.Entities.FirstOrDefaultAsync(c => c.Id == model.Id, cancellationToken);
            attandance.DepartureDate = DateTime.Now;
            attandance.DepartureTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            await _attendanceListRepository.UpdateAsync(attandance, cancellationToken);
            return RedirectToAction(nameof(Index), new { Id = model.UserId });
        }
    }
}