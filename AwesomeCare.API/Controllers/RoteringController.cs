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
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
        private AwesomeCareDbContext _dbContext;

        public RoteringController(ILogger<RoteringController> logger, IGenericRepository<ClientRota> clientRotaRepository,
            IGenericRepository<ClientRotaType> clientRotaTypeRepository, IGenericRepository<Client> clientRepository,
             IGenericRepository<ClientRotaDays> clientRotaDaysRepository, IGenericRepository<Rota> rotaRepository,
              IGenericRepository<RotaDayofWeek> rotaDayofWeekRepository, IGenericRepository<StaffRota> staffRotaRepository,
             IGenericRepository<StaffRotaPeriod> staffRotaPeriodRepository, IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
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

            var rotas = (from sr in _staffRotaRepository.Table
                         join srp in _staffRotaPeriodRepository.Table on sr.StaffRotaId equals srp.StaffRotaId
                         join st in _staffPersonalInfoRepository.Table on sr.Staff equals st.StaffPersonalInfoId
                         join crd in _clientRotaDaysRepository.Table on new { key1 = sr.RotaId, key2 = sr.RotaDayofWeekId.Value } equals new { key1 = crd.RotaId, key2 = crd.RotaDayofWeekId }
                         join rd in _rotaDayofWeekRepository.Table on crd.RotaDayofWeekId equals rd.RotaDayofWeekId
                         join r in _rotaRepository.Table on crd.RotaId equals r.RotaId
                         join cr in _clientRotaRepository.Table on crd.ClientRotaId equals cr.ClientRotaId
                         join crt in _clientRotaTypeRepository.Table on cr.ClientRotaTypeId equals crt.ClientRotaTypeId
                         join c in _clientRepository.Table on cr.ClientId equals c.ClientId
                         where sr.RotaDate >= startDate && sr.RotaDate <= startDate
                         select new LiveTracker
                         {
                             AreaCode = c.AreaCodeId,
                             ClientRotaId = cr.ClientRotaId,
                             ClientId = cr.ClientId,
                             ClientProviderReference = c.ProviderReference,
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
                             ClientKeySafe = c.KeySafe,
                             ClientRate = c.Rate,
                             ClientTelephone = c.Telephone,
                             ClockInMethod = "",
                             ClockOutMethod = "",
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

            return Ok(rotas);
        }

        [HttpGet]
        [Route("GetStaffRotaPeriodById/{id}")]
        [ProducesResponseType(typeof(GetStaffRotaPeriodForEdit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStaffRotaPeriod(int id)
        {
            var entity =await _staffRotaPeriodRepository.GetEntity(id);
            if (entity == null) return NotFound();
            var mappedEntity = Mapper.Map<GetStaffRotaPeriodForEdit>(entity);
            return Ok(mappedEntity);
        }

        [HttpPut]
        [Route("PatchStaffRotaPeriod")]
        [ProducesResponseType(typeof(GetStaffRotaPeriodForEdit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchStaffRotaPeriod([FromBody]EditStaffRotaPeriod model)
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
                         }).OrderBy(o => o.RotaDate).ToList();

            return rotas;
        }

    }
}