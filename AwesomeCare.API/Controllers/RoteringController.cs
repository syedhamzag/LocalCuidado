using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Rotering;
using AwesomeCare.DataTransferObject.DTOs.StaffRotaPeriod;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AwesomeCare.DataTransferObject.EqualityComparers;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    // [AllowAnonymous]
    public class RoteringController : ControllerBase
    {
        private ILogger<RoteringController> _logger;
        private IGenericRepository<ClientRota> _clientRotaRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<ClientRotaDays> _clientRotaDaysRepository;
        private IGenericRepository<Rota> _rotaRepository;
        private IGenericRepository<RotaDayofWeek> _rotaDayofWeekRepository;
        private IGenericRepository<StaffRota> _staffRotaRepository;
        private IGenericRepository<StaffPersonalInfo> _staffPersonalInfoRepository;
        private IGenericRepository<StaffRotaPeriod> _staffRotaPeriodRepository;
        private readonly IGenericRepository<ClientRotaTask> clientRotaTaskRepository;
        private readonly IGenericRepository<RotaTask> rotaTaskRepository;
        private readonly IGenericRepository<ShiftBooking> shiftBookingRepository;
        private readonly IGenericRepository<StaffShiftBooking> staffShiftBookingRepository;
        private readonly IGenericRepository<RotaDayofWeek> rotaDayOfWeekRepository;
        private AwesomeCareDbContext _dbContext;

        public RoteringController(ILogger<RoteringController> logger, IGenericRepository<ClientRota> clientRotaRepository,
            IGenericRepository<ClientRotaType> clientRotaTypeRepository, IGenericRepository<Client> clientRepository,
             IGenericRepository<ClientRotaDays> clientRotaDaysRepository, IGenericRepository<Rota> rotaRepository,
              IGenericRepository<RotaDayofWeek> rotaDayofWeekRepository, IGenericRepository<StaffRota> staffRotaRepository,
             IGenericRepository<StaffRotaPeriod> staffRotaPeriodRepository,
             IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
             IGenericRepository<ClientRotaTask> clientRotaTaskRepository,
             IGenericRepository<RotaTask> rotaTaskRepository,
             IGenericRepository<ShiftBooking> shiftBookingRepository,
             IGenericRepository<RotaDayofWeek> rotaDayOfWeekRepository,
             IGenericRepository<StaffShiftBooking> staffShiftBookingRepository,
             AwesomeCareDbContext dbContext)
        {
            _logger = logger;
            _clientRotaRepository = clientRotaRepository;
            _clientRotaTypeRepository = clientRotaTypeRepository;
            _clientRepository = clientRepository;
            _clientRotaDaysRepository = clientRotaDaysRepository;
            _rotaRepository = rotaRepository;
            _rotaDayofWeekRepository = rotaDayofWeekRepository;
            _staffRotaRepository = staffRotaRepository;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
            _staffRotaPeriodRepository = staffRotaPeriodRepository;
            this.clientRotaTaskRepository = clientRotaTaskRepository;
            this.rotaTaskRepository = rotaTaskRepository;
            this.shiftBookingRepository = shiftBookingRepository;
            this.staffShiftBookingRepository = staffShiftBookingRepository;
            this.rotaDayOfWeekRepository = rotaDayOfWeekRepository;
            _dbContext = dbContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDate">StartDate (yyyy-MM-dd)</param>
        /// <param name="eDate">EndDate (yyyy-MM-dd)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("RotaAdmin/{sDate}/{eDate}")]
        [ProducesResponseType(typeof(List<RotaAdmin>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Rota(string sDate, string eDate)
        {
            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            bool isEndDateValid = DateTime.TryParseExact(eDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime endDate);
            if (!isStartDateValid || !isEndDateValid)
            {
                return BadRequest($"Invalid Date format, Format is {format}");
            }
            //var rotas = (from cr in _clientRotaRepository.Table
            //             join crt in _clientRotaTypeRepository.Table on cr.ClientRotaTypeId equals crt.ClientRotaTypeId
            //             join c in _clientRepository.Table on cr.ClientId equals c.ClientId
            //             select new RotaAdmin
            //             {
            //                 ClientRotaId = cr.ClientRotaId,
            //                 ClientId = cr.ClientId,
            //                 Period = crt.RotaType,
            //                 ClientName = c.Firstname + " " + c.Middlename + " " + c.Surname,
            //                 ClientPostCode = c.PostCode,
            //                 ClientKeySafe = c.KeySafe,
            //                 RotaDays = (from crd in cr.ClientRotaDays
            //                                 // join r in _rotaRepository.Table on new {rotaId = crd.RotaId, dayofweekId = crd.RotaDayofWeekId } crd.RotaId equals r.RotaId
            //                                 //  join rd in _rotaDayofWeekRepository.Table on crd.RotaDayofWeekId equals rd.RotaDayofWeekId
            //                             join sr in _staffRotaRepository.Table on new { key1 = crd.RotaId, key2 = crd.RotaDayofWeekId } equals new { key1 = sr.RotaId, key2 = sr.RotaDayofWeekId.Value }
            //                             join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
            //                             join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
            //                             where sr.RotaDate >= startDate && sr.RotaDate <= endDate && srp.ClientRotaTypeId == cr.ClientRotaTypeId
            //                             select new RotaDays
            //                             {

            //                                 StartTime = crd.StartTime,
            //                                 StopTime = crd.StopTime,
            //                                 ClockInTime = srp.ClockInTime,
            //                                 ClockOutTime = srp.ClockOutTime,
            //                                 Rota = "",// r.RotaName,
            //                                 Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
            //                                 RotaDate = sr.RotaDate,
            //                                 Remark = sr.Remark,
            //                                 ReferenceNumber = sr.ReferenceNumber,
            //                                 Partners = (from p in sr.StaffRotaPartners
            //                                             join stp in _staffPersonalInfoRepository.Table on p.StaffId equals stp.StaffPersonalInfoId
            //                                             select new StaffPartner
            //                                             {
            //                                                 Partner = stp.FirstName + " " + stp.MiddleName + " " + stp.LastName,
            //                                                 Telephone = stp.Telephone
            //                                             }).ToList()
            //                             }).OrderByDescending(o => o.RotaDate).ToList()

            //             }).ToList();

            var rotas = GetRotaAdmins(startDate, endDate);

            return Ok(rotas);
        }

        [HttpGet]
        [Route("LiveRota/{date}")]
        [ProducesResponseType(typeof(List<LiveTracker>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult LiveRota(string date)
        {
            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(date, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            if (!isStartDateValid)
            {
                return BadRequest($"Invalid Date format, Format is {format}");
            }

            var dayOfWeek = startDate.Date.DayOfWeek.ToString();

            var weekDayId = rotaDayOfWeekRepository.Table.FirstOrDefault(r => r.DayofWeek == dayOfWeek)?.RotaDayofWeekId;

            var rotas = (//from sr in _staffRotaRepository.Table
                         //join shiftBooking in shiftBookingRepository.Table on sr.RotaId equals shiftBooking.Rota
                         //join stafShiftBooking in staffShiftBookingRepository.Table on shiftBooking.ShiftBookingId equals stafShiftBooking.ShiftBookingId
                         //join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
                         //join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                         //join crd in _clientRotaDaysRepository.Table on new { key1 = sr.RotaId, key2 = sr.RotaDayofWeekId.Value } equals new { key1 = crd.RotaId, key2 = crd.RotaDayofWeekId }
                         //join rd in _rotaDayofWeekRepository.Table on crd.RotaDayofWeekId equals rd.RotaDayofWeekId
                         //join r in _rotaRepository.Table on crd.RotaId equals r.RotaId
                         //join cr in _clientRotaRepository.Table on crd.ClientRotaId equals cr.ClientRotaId
                         //join crt in _clientRotaTypeRepository.Table on cr.ClientRotaTypeId equals crt.ClientRotaTypeId
                         //join c in _clientRepository.Table on cr.ClientId equals c.ClientId
                         from sr in _staffRotaRepository.Table
                         join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
                         join crd in _clientRotaDaysRepository.Table on new { key1 = srp.ClientRotaTypeId, key2 = srp.ClientId } equals new { key1 = crd.ClientRotaTypeId.GetValueOrDefault(), key2 = crd.ClientId }
                         join c in _clientRepository.Table on srp.ClientId equals c.ClientId
                         join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                         join crt in _clientRotaTypeRepository.Table on srp.ClientRotaTypeId equals crt.ClientRotaTypeId
                         join r in _rotaRepository.Table on sr.RotaId equals r.RotaId
                         join rtwd in rotaDayOfWeekRepository.Table on crd.RotaDayofWeekId equals rtwd.RotaDayofWeekId
                         where sr.RotaDate >= startDate && sr.RotaDate <= startDate && rtwd.RotaDayofWeekId == weekDayId
                         select new LiveTracker
                         {
                             AreaCode = c.AreaCodeId,
                             ClientRotaId = crd.ClientRotaId,
                             ClientId = srp.ClientId.GetValueOrDefault(),
                             ClientProviderReference = c.ProviderReference,
                             Period = crt.RotaType,
                             ClientName = c.Firstname + " " + c.Middlename + " " + c.Surname,
                             ClientPostCode = c.PostCode,
                             RotaDate = sr.RotaDate,
                             DayofWeek = rtwd.DayofWeek,
                             StartTime = crd.StartTime,// crd.StartTime,
                             StopTime = crd.StopTime,// crd.StopTime,
                             ClockInTime = srp.ClockInTime,
                             ClockOutTime = srp.ClockOutTime,
                             Rota = r.RotaName,
                             Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                             Remark = sr.Remark,
                             ReferenceNumber = sr.ReferenceNumber,
                             ClientKeySafe = c.KeySafe,
                             ClientRate = c.Rate,
                             ClientTelephone = c.Telephone,
                             ClockInMethod = srp.ClockInMode,
                             ClockOutMethod = srp.ClockOutMode,
                             Feedback = srp.Feedback,
                             HandOver = srp.HandOver,
                             Comment = srp.Comment,
                             ClockInAddress = srp.ClockInAddress,
                             ClockOutAddress = srp.ClockOutAddress,
                             NumberOfStaff = c.NumberOfStaff,
                             StaffTelephone = st.Telephone,
                             StaffRate = st.Rate,
                             ClientRotaDaysId = crd.ClientRotaDaysId,
                             StaffRotaId = sr.StaffRotaId,
                             StaffRotaPeriodId = srp.StaffRotaPeriodId
                         }).OrderBy(o => o.RotaDate).Distinct().ToList();

            return Ok(rotas);
        }

        [HttpGet,AllowAnonymous]
        [Route("GetStaffRotaPeriodById/{id}")]
        [ProducesResponseType(typeof(GetStaffRotaPeriodForEdit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffRotaPeriod(int id)
        {
            var entity = await _staffRotaPeriodRepository.GetEntity(id);
            if (entity == null) return NotFound();
            var mappedEntity = Mapper.Map<GetStaffRotaPeriodForEdit>(entity);
            return Ok(mappedEntity);
        }

        [HttpPut]
        [Route("PatchStaffRotaPeriod")]
        [ProducesResponseType(typeof(GetStaffRotaPeriodForEdit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchStaffRotaPeriod([FromBody] EditStaffRotaPeriod model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            //  var entitySet = _dbContext.Set<StaffRotaPeriod>();
            var entity = await _staffRotaPeriodRepository.GetEntity(model.StaffRotaPeriodId);
            if (entity == null) return NotFound();

            var mappedEntity = Mapper.Map<EditStaffRotaPeriod, StaffRotaPeriod>(model, entity);

            var result = await _staffRotaPeriodRepository.UpdateEntity(mappedEntity);
            var getEntity = Mapper.Map<GetStaffRotaPeriodForEdit>(result);
            return Ok(getEntity);
        }

        /// <summary>
        /// Date Format is yyyy-MM-dd
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("StaffRota2/{staffId}/{searchDate}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult StaffRota2(int staffId, string searchDate)
        {
            string format = "yyyy-MM-dd";
            bool isSearchDateValid = DateTime.TryParseExact(searchDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime sDate);

            if (!isSearchDateValid)
            {
                return BadRequest($"Invalid Date format, Format is {format}");
            }

            // int staffId = 2;


            var staffRotas = (from crtd in _clientRotaDaysRepository.Table
                              join crt in _clientRotaRepository.Table on crtd.ClientRotaId equals crt.ClientRotaId
                              join crtt in _clientRotaTypeRepository.Table on crt.ClientRotaTypeId equals crtt.ClientRotaTypeId
                              join cl in _clientRepository.Table on crt.ClientId equals cl.ClientId
                              join strt in _staffRotaRepository.Table on crtd.RotaId equals strt.RotaId
                              join strtp in _staffRotaPeriodRepository.Table on strt.StaffRotaId equals strtp.StaffRotaId
                              join cltk in clientRotaTaskRepository.Table on crtd.ClientRotaDaysId equals cltk.ClientRotaDaysId
                              // join tk in rotaTaskRepository.Table on cltk.RotaTaskId equals tk.RotaTaskId
                              where strt.RotaDate == sDate && strt.Staff == staffId
                              select new
                              {
                                  StaffRotaId = strt.StaffRotaId,
                                  StaffRotaPeriodId = strtp.StaffRotaPeriodId,
                                  StartTime = crtd.StartTime,
                                  StopTime = crtd.StopTime,
                                  ClockInTime = strtp.ClockInTime,
                                  ClockOutTime = strtp.ClockOutTime,
                                  Comment = strtp.Comment,
                                  Feedback = strtp.Feedback,
                                  RotaId = crtd.RotaId,
                                  RotaType = crtt.RotaType,
                                  ClientId = cl.ClientId,
                                  ClientFirstName = cl.Firstname,
                                  ClientMiddleName = cl.Middlename,
                                  ClientSurName = cl.Surname,
                                  ClientAddress = "",
                                  ClientKeySafeNumber = cl.KeySafe,
                                  ClientPostCode = cl.PostCode,
                                  ClientTelephone = cl.Telephone,
                                  Client = $"{cl.Firstname} {cl.Middlename} {cl.Surname}",
                                  ClientUniqueId = cl.UniqueId,
                                  ClientLatitude = cl.Latitude,
                                  ClientLongitude = cl.Longitude,
                                  RotaDate = strt.RotaDate,
                                  StaffId = strt.Staff,
                                  ReferenceNumber = strt.ReferenceNumber,
                                  Tasks = (from tsk in crtd.ClientRotaTask
                                           join tk in rotaTaskRepository.Table on tsk.RotaTaskId equals tk.RotaTaskId
                                           select new
                                           {
                                               RotaTaskId = tk.RotaTaskId,
                                               TaskName = tk.TaskName,
                                               GivenAcronym = tk.GivenAcronym,
                                               NotGivenAcronym = tk.NotGivenAcronym
                                           }).ToList(),
                                  Partners = (from str in strt.StaffRotaPartners
                                              join stp in _staffPersonalInfoRepository.Table on str.StaffId equals stp.StaffPersonalInfoId
                                              select new
                                              {
                                                  Partner = stp.FirstName + " " + stp.MiddleName + " " + stp.LastName,
                                                  Telephone = stp.Telephone
                                              }).ToList()
                              }).ToList();

            var groupedRota = (from rt in staffRotas
                               group rt by rt.RotaType into rtgp
                               select new
                               {
                                   RotaType = rtgp.Key,
                                   Items = rtgp.ToList()
                               }).ToList();

            return Ok(groupedRota);
        }

        [HttpGet]
        [Route("StaffRota/{staffId}/{searchDate}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult StaffRota(int staffId, string searchDate)
        {
            string format = "yyyy-MM-dd";
            bool isSearchDateValid = DateTime.TryParseExact(searchDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime sDate);

            if (!isSearchDateValid)
            {
                return BadRequest($"Invalid Date format, Format is {format}");
            }

            // int staffId = 2;
            var dayOfWeek = sDate.Date.DayOfWeek.ToString();

            var weekDayId = rotaDayOfWeekRepository.Table.FirstOrDefault(r => r.DayofWeek == dayOfWeek)?.RotaDayofWeekId;



            var staffRotas = (from  sr in _staffRotaRepository.Table 
                              join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
                              join crd in _clientRotaDaysRepository.Table on new { key1 = srp.ClientRotaTypeId, key2 = srp.ClientId } equals new { key1 = crd.ClientRotaTypeId.GetValueOrDefault(), key2 = crd.ClientId }
                              join c in _clientRepository.Table on srp.ClientId equals c.ClientId                              
                              join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                              join crt in _clientRotaTypeRepository.Table on srp.ClientRotaTypeId equals crt.ClientRotaTypeId
                              join r in _rotaRepository.Table on sr.RotaId equals r.RotaId
                              join rtwd in rotaDayOfWeekRepository.Table on crd.RotaDayofWeekId equals rtwd.RotaDayofWeekId
                              where sr.RotaDate >= sDate && sr.RotaDate <= sDate && sr.Staff == staffId && rtwd.RotaDayofWeekId == weekDayId
                              select new
                              {
                                  StaffRotaId = sr.StaffRotaId,
                                  RotaDate = sr.RotaDate,
                                  StaffId = sr.Staff,
                                  Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                  StaffTelephone = st.Telephone,
                                  StaffRate = st.Rate,
                                  ReferenceNumber = sr.ReferenceNumber,
                                  Remark = sr.Remark,
                                  RotaId = sr.RotaId,
                                  Rota = r.RotaName,
                                  DayOfWeek = rtwd.DayofWeek,
                                  RotaDayOfWeekId = sr.RotaDayofWeekId,
                                  StaffRotaPeriodId = srp.StaffRotaPeriodId,
                                  ClockInTime = srp.ClockInTime,
                                  ClockOutTime = srp.ClockOutTime,
                                  ClockInMethod = srp.ClockInMode,
                                  ClockOutMethod = srp.ClockOutMode,
                                  Feedback = srp.Feedback,
                                  HandOver = srp.HandOver,
                                  Comment = srp.Comment,
                                  ClockInAddress = srp.ClockInAddress,
                                  ClockOutAddress = srp.ClockOutAddress,
                                  Period = crt.RotaType,
                                  ClientRotaTypeId = srp.ClientRotaTypeId,
                                 // ClientRotaId = cr.ClientRotaId,
                                  ClientRotaDaysId = crd.ClientRotaDaysId,
                                  StartTime = crd.StartTime,
                                  StopTime = crd.StopTime,
                                  ClientName = c.Firstname + " " + c.Middlename + " " + c.Surname,
                                  ClientProviderReference = c.ProviderReference,
                                  ClientId = c.ClientId,
                                  ClientUniqueId = c.UniqueId,
                                  AreaCode = c.AreaCodeId,
                                  ClientKeySafe = c.KeySafe,
                                  ClientPostCode = c.PostCode,
                                  ClientRate = c.Rate,
                                  ClientTelephone = c.Telephone,
                                  NumberOfStaff = c.NumberOfStaff,
                                  Partners = (from p in sr.StaffRotaPartners
                                              join strp in _staffPersonalInfoRepository.Table on p.StaffId equals strp.StaffPersonalInfoId
                                              select new
                                              {
                                                  Partner = strp.FirstName + " " + strp.MiddleName + " " + strp.LastName,
                                                  Telephone = strp.Telephone
                                              }).ToList(),
                                  Tasks = (from tsk in crd.ClientRotaTask
                                           join tk in rotaTaskRepository.Table on tsk.RotaTaskId equals tk.RotaTaskId
                                           select new
                                           {
                                               RotaTaskId = tk.RotaTaskId,
                                               TaskName = tk.TaskName,
                                               GivenAcronym = tk.GivenAcronym,
                                               NotGivenAcronym = tk.NotGivenAcronym
                                           }).ToList()
                              }).ToList();


            // var test = staffRotas.Where(r => r.ClientRotaId == 310).ToList();

            var groupedRota = (from rt in staffRotas
                               group rt by rt.Period into rtgp
                               select new DataTransferObject.DTOs.Rotering.GetStaffRota
                               {
                                   RotaType = rtgp.Key,
                                   Items = (from cl in rtgp.ToList()
                                            select new Item
                                            {
                                                AreaCode = cl.AreaCode,
                                                ClientId = cl.ClientId,
                                                ClientKeySafe = cl.ClientKeySafe,
                                                Client = cl.ClientName,
                                                ClientPostCode = cl.ClientPostCode,
                                                ClientProviderReference = cl.ClientProviderReference,
                                                ClientRate = cl.ClientRate,
                                                ClientRotaDaysId = cl.ClientRotaDaysId,
                                                ClientTelephone = cl.ClientTelephone,
                                                ClientUniqueId = cl.ClientUniqueId,
                                                ClockInAddress = cl.ClockInAddress,
                                                ClockInMethod = cl.ClockInMethod,
                                                ClockInTime = cl.ClockInTime,
                                                ClockOutAddress = cl.ClockOutAddress,
                                                ClockOutMethod = cl.ClockOutMethod,
                                                ClockOutTime = cl.ClockOutTime,
                                                Comment = cl.Comment,
                                                Feedback = cl.Feedback,
                                                HandOver = cl.HandOver,
                                                NumberOfStaff = cl.NumberOfStaff,
                                                Partners = (from p in cl.Partners
                                                            select new DataTransferObject.DTOs.Rotering.StaffRotaPartner
                                                            {
                                                                Partner = p.Partner,
                                                                Telephone = p.Telephone
                                                            }).ToList(),
                                                Period = cl.Period,
                                                ReferenceNumber = cl.ReferenceNumber,
                                                Remark = cl.Remark,
                                                Rota = cl.Rota,
                                                RotaDate = cl.RotaDate,
                                                RotaId = cl.RotaId,
                                                Staff = cl.Staff,
                                                StaffRate = cl.StaffRate,
                                                StaffRotaId = cl.StaffRotaId,
                                                StaffRotaPeriodId = cl.StaffRotaPeriodId,
                                                StaffTelephone = cl.StaffTelephone,
                                                StartTime = cl.StartTime,
                                                StopTime = cl.StopTime,
                                                DayofWeek = cl.DayOfWeek,
                                                RotaDayOfWeekId = cl.RotaDayOfWeekId,
                                              //  ClientRotaId = cl.ClientRotaId,
                                                Tasks = (from t in cl.Tasks
                                                         select new DataTransferObject.DTOs.Rotering.Task
                                                         {
                                                             GivenAcronym = t.GivenAcronym,
                                                             NotGivenAcronym = t.NotGivenAcronym,
                                                             RotaTaskId = t.RotaTaskId,
                                                             TaskName = t.TaskName
                                                         }).ToList()

                                            }
                                            ).Distinct(new GetStaffRotaItemEqualityComparer()).ToList()
                               }).ToList();

            return Ok(groupedRota);

            //return Ok();
        }

        [AllowAnonymous]
        [HttpPost("ClockInClockOut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ClockInClockOut([FromBody] StaffClockInClockOut model)
        {
            //try
            //{
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rota = await _staffRotaPeriodRepository.Table.FirstOrDefaultAsync(r => r.StaffRotaPeriodId == model.StaffRotaPeriodId);

            rota.Feedback = model.Feedback;
            foreach (var item in model.StaffRotaTasks)
            {
                rota.StaffRotaTasks.Add(new StaffRotaTask
                {
                    StaffRotaPeriodId = model.StaffRotaPeriodId,
                    RotaTaskId = item.RotaTaskId,
                    IsGiven = item.IsGiven
                });
            }

            var id = await _staffRotaPeriodRepository.UpdateEntity(rota);

            return Ok();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "", null);
            //    return BadRequest(ex.Message);
            //}
        }

        [HttpPost("ScanQr/ClockIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ScanQrCodeClockIn(string rotaId, string distance, string geolocation)
        {

            int staffRotaId = int.TryParse(rotaId, out int rtId) ? rtId : 0;
            var rota = await _staffRotaPeriodRepository.Table.FirstOrDefaultAsync(r => r.StaffRotaPeriodId == staffRotaId);
            if (rota == null)
                return NotFound();


            rota.ClockInTime = DateTimeOffset.UtcNow;
            rota.ClockInMode = ClockModeEnum.ScanCode.ToString();
            rota.ClockInAddress = geolocation;

            var result = await _staffRotaPeriodRepository.UpdateEntity(rota);

            if (result != null)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("ScanQr/ClockOut")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ScanQrCodeClockOut(string rotaId, string distance, string geolocation)
        {

            int staffRotaId = int.TryParse(rotaId, out int rtId) ? rtId : 0;
            var rota = await _staffRotaPeriodRepository.Table.FirstOrDefaultAsync(r => r.StaffRotaPeriodId == staffRotaId);
            if (rota == null)
                return NotFound();

            rota.ClockOutTime = DateTimeOffset.UtcNow;
            rota.ClockOutMode = ClockModeEnum.ScanCode.ToString();
            rota.ClockOutAddress = geolocation;

            var result = await _staffRotaPeriodRepository.UpdateEntity(rota);

            if (result != null)
                return Ok();
            else
                return BadRequest();
        }


        List<RotaAdmin> GetRotaAdmins(DateTime startDate, DateTime endDate)
        {
            var rotas = (from sr in _staffRotaRepository.Table
                         join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
                         join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                         join crd in _clientRotaDaysRepository.Table on new { key1 = sr.RotaId, key2 = sr.RotaDayofWeekId.Value } equals new { key1 = crd.RotaId, key2 = crd.RotaDayofWeekId }
                         join rd in _rotaDayofWeekRepository.Table on crd.RotaDayofWeekId equals rd.RotaDayofWeekId
                         join r in _rotaRepository.Table on crd.RotaId equals r.RotaId
                         join cr in _clientRotaRepository.Table on crd.ClientRotaId equals cr.ClientRotaId
                         join crt in _clientRotaTypeRepository.Table on cr.ClientRotaTypeId equals crt.ClientRotaTypeId
                         join c in _clientRepository.Table on cr.ClientId equals c.ClientId
                         where sr.RotaDate >= startDate && sr.RotaDate <= endDate
                         select new RotaAdmin
                         {
                             ClientRotaId = cr.ClientRotaId,
                             ClientId = cr.ClientId,
                             Period = crt.RotaType,
                             ClientName = c.Firstname + " " + c.Middlename + " " + c.Surname,
                             ClientPostCode = c.PostCode,
                             RotaDate = sr.RotaDate,
                             DayofWeek = rd.DayofWeek,
                             StartTime = crd.StartTime,
                             StopTime = crd.StopTime,
                             ClockInTime = srp.ClockInTime,
                             ClockOutTime = srp.ClockOutTime,
                             Rota = r.RotaName,
                             Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                             Remark = sr.Remark,
                             ReferenceNumber = sr.ReferenceNumber,
                             Partners = (from p in sr.StaffRotaPartners
                                         join stp in _staffPersonalInfoRepository.Table on p.StaffId equals stp.StaffPersonalInfoId
                                         select new StaffPartner
                                         {
                                             Partner = stp.FirstName + " " + stp.MiddleName + " " + stp.LastName,
                                             Telephone = stp.Telephone
                                         }).ToList()
                         }).OrderByDescending(o => o.RotaDate).Distinct().ToList();

            return rotas;
        }

        [HttpGet]
        [Route("LiveRota2/{sdate}/{edate}")]
        [ProducesResponseType(typeof(List<LiveTracker>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult LiveRota2(string sdate, string edate)
        {
            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sdate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            if (!isStartDateValid)
            {
                return BadRequest($"Invalid StartDate format, Format is {format}");
            }

            bool isStopDateValid = DateTime.TryParseExact(edate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime stopDate);
            if (!isStopDateValid)
            {
                return BadRequest($"Invalid StopDate format, Format is {format}");
            }

            var rotas = (from sr in _staffRotaRepository.Table
                         join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
                         join crd in _clientRotaDaysRepository.Table on new { key1 = srp.ClientRotaTypeId, key2 = srp.ClientId } equals new { key1 = crd.ClientRotaTypeId.GetValueOrDefault(), key2 = crd.ClientId }
                         join c in _clientRepository.Table on srp.ClientId equals c.ClientId
                         join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                         join crt in _clientRotaTypeRepository.Table on srp.ClientRotaTypeId equals crt.ClientRotaTypeId
                         join r in _rotaRepository.Table on sr.RotaId equals r.RotaId
                         join rtwd in rotaDayOfWeekRepository.Table on crd.RotaDayofWeekId equals rtwd.RotaDayofWeekId
                         where sr.RotaDate >= startDate && sr.RotaDate <= stopDate// && rtwd.RotaDayofWeekId == weekDayId
                         select new LiveTracker
                         {
                             AreaCode = c.AreaCodeId,
                             ClientRotaId = crd.ClientRotaId,
                             ClientId = srp.ClientId.GetValueOrDefault(),
                             ClientProviderReference = c.ProviderReference,
                             Period = crt.RotaType,
                             ClientName = c.Firstname + " " + c.Middlename + " " + c.Surname,
                             ClientPostCode = c.PostCode,
                             RotaDate = sr.RotaDate,
                             //  DayofWeek = rd.DayofWeek,
                             StartTime = crd.StartTime,
                             StopTime = crd.StopTime,
                             ClockInTime = srp.ClockInTime,
                             ClockOutTime = srp.ClockOutTime,
                             Rota = r.RotaName,
                             Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                             Remark = sr.Remark,
                             ReferenceNumber = sr.ReferenceNumber,
                             ClientKeySafe = c.KeySafe,
                             ClientRate = c.Rate,
                             ClientTelephone = c.Telephone,
                             ClockInMethod = srp.ClockInMode,
                             ClockOutMethod = srp.ClockOutMode,
                             Feedback = srp.Feedback,
                             HandOver = srp.HandOver,
                             Comment = srp.Comment,
                             ClockInAddress = srp.ClockInAddress,
                             ClockOutAddress = srp.ClockOutAddress,
                             NumberOfStaff = c.NumberOfStaff,
                             StaffTelephone = st.Telephone,
                             StaffRate = st.Rate,
                             ClientRotaDaysId = crd.ClientRotaDaysId,
                             StaffRotaId = sr.StaffRotaId,
                             StaffRotaPeriodId = srp.StaffRotaPeriodId
                         }).OrderBy(o => o.RotaDate).ToList();

            var distinctRotas = rotas.Distinct(new LiveTrackerEqualityComparer()).ToList();

            return Ok(distinctRotas);
        }

        /// <summary>
        /// Delete StaffRota Period by Id
        /// </summary>
        /// <param name="staffRotaPeriodId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteStaffRotaPeriod")]
        public async Task<IActionResult> DeleteStaffRotaPeriod(int staffRotaPeriodId)
        {
            var staffRotaPeriod = await _staffRotaPeriodRepository.GetEntity(staffRotaPeriodId);
            if (staffRotaPeriod == null) return NotFound();

            await _staffRotaPeriodRepository.DeleteEntity(staffRotaPeriod);

            return Ok();
        }

        /// <summary>
        /// Get Clients attached to the specified Rota
        /// </summary>
        /// <param name="rotaId"></param>
        /// <param name="rotaDayOfWeekId"></param>
        /// <param name="clientRotaTypeId"></param>
        /// <returns></returns>
        [HttpGet("Rota/AttachedClient/{rotaId}/{rotaDayOfWeekId}/{clientRotaTypeId}")]
        [ProducesResponseType(typeof(List<GetClientAttachedToRota>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAttachedClientByRotaId(int rotaId, int rotaDayOfWeekId,int clientRotaTypeId)
        {

            //Get Clients attached to the specified Rota
            var clientByRota = await (from cl in _clientRotaRepository.Table
                                      join cld in _clientRotaDaysRepository.Table on cl.ClientRotaId equals cld.ClientRotaId
                                      where cld.RotaId == rotaId && cld.RotaDayofWeekId == rotaDayOfWeekId && cl.ClientRotaTypeId == clientRotaTypeId
                                      select new GetClientAttachedToRota
                                      {
                                          ClientRotaId = cl.ClientRotaId,
                                          ClientId = cl.ClientId,
                                          ClientRotaTypeId = cl.ClientRotaTypeId,
                                          RotaId = cld.RotaId,
                                          RotaDayofWeekId = cld.RotaDayofWeekId,
                                          ClientRotaDaysId = cld.ClientRotaDaysId
                                      }).ToListAsync();


            return Ok(clientByRota);
        }


    }
}