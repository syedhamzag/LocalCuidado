using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.OfficeAttendance;
using AwesomeCare.DataTransferObject.DTOs.StaffOfficeLocation;
using AwesomeCare.DataTransferObject.EqualityComparers;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OfficeAttendanceController : ControllerBase
    {
        private IGenericRepository<OfficeAttendance> _officeAttendanceRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<StaffOfficeLocation> _stafflocationRepository;
        private IGenericRepository<OfficeLocation> _locationRepository;

        public OfficeAttendanceController(IGenericRepository<OfficeAttendance> OfficeAttendanceRepository, IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<StaffOfficeLocation> stafflocationRepository, IGenericRepository<OfficeLocation> locationRepository)
        {

            _officeAttendanceRepository = OfficeAttendanceRepository;
            _staffRepository = staffRepository;
            _locationRepository = locationRepository;
            _stafflocationRepository = stafflocationRepository;
        }
        #region OfficeAttendance
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetAttendance>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _officeAttendanceRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostAttendance post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var OfficeAttendance = Mapper.Map<OfficeAttendance>(post);
            await _officeAttendanceRepository.InsertEntity(OfficeAttendance);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutAttendance models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var OfficeAttendance = Mapper.Map<OfficeAttendance>(models);
            await _officeAttendanceRepository.UpdateEntity(OfficeAttendance);
            return Ok();

        }

        [HttpGet("GetByStaff/{id}")]
        [ProducesResponseType(type: typeof(GetAttendance), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByStaff(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from st in _staffRepository.Table
                                   join c in _officeAttendanceRepository.Table on st.StaffPersonalInfoId equals c.Staff
                                   where c.Staff == id.Value
                                   select new GetAttendance
                                   {
                                        AttendanceId = c.AttendanceId,
                                        Date = c.Date,
                                        OfficeLocation = (from t in st.StaffOfficeLocation
                                                          join ol in _locationRepository.Table on t.Id equals ol.OfficeLocationId
                                                          where ol.OfficeLocationId == t.Id
                                                          select new GetStaffOfficeLocation
                                                          {
                                                            LocationName = ol.Address
                                                          }).ToList(),
                                        JobTitle = c.JobTitle,
                                        StaffName = string.Concat(st.FirstName," ",st.MiddleName," ",st.LastName),
                                        Location = c.Location,
                                        ClockInAddress = c.ClockInAddress,
                                        ClockInDistance = c.ClockInDistance,
                                        ClockOutAddress = c.ClockOutAddress,
                                        ClockOutDistance = c.ClockOutDistance,
                                        StartTime = c.StartTime,
                                        StopTime = c.StopTime,
                                        ClockIn = c.ClockIn,
                                        ClockOut = c.ClockOut,
                                        ClockInMethod = c.ClockInMethod,
                                        ClockOutMethod = c.ClockOutMethod,
                                        ClockDiff = c.ClockDiff,
                                        Remark = c.Remark
    }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetAttendance), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from st in _staffRepository.Table
                                   join c in _officeAttendanceRepository.Table on st.StaffPersonalInfoId equals c.Staff
                                   where c.AttendanceId == id.Value
                                   select new GetAttendance
                                   {
                                       AttendanceId = c.AttendanceId,
                                       Date = c.Date,
                                       OfficeLocation = (from t in st.StaffOfficeLocation
                                                         join ol in _locationRepository.Table on t.Id equals ol.OfficeLocationId
                                                         where ol.OfficeLocationId == t.Id
                                                         select new GetStaffOfficeLocation
                                                         {
                                                             Id = t.Id,
                                                             Location = t.Location,
                                                             Staff = t.Staff,
                                                             LocationName = ol.Address
                                                         }).ToList(),
                                       JobTitle = c.JobTitle,
                                       Staff = c.Staff,
                                       StaffName = string.Concat(st.FirstName, " ", st.MiddleName, " ", st.LastName),
                                       Location = c.Location,
                                       ClockInAddress = c.ClockInAddress,
                                       ClockInDistance = c.ClockInDistance,
                                       ClockOutAddress = c.ClockOutAddress,
                                       ClockOutDistance = c.ClockOutDistance,
                                       StartTime = c.StartTime,
                                       StopTime = c.StopTime,
                                       ClockIn = c.ClockIn,
                                       ClockOut = c.ClockOut,
                                       ClockInMethod = c.ClockInMethod,
                                       ClockOutMethod = c.ClockOutMethod,
                                       ClockDiff = c.ClockDiff,
                                       Remark = c.Remark
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
        [HttpGet("GetByDate/{sdate}/{edate}")]
        [ProducesResponseType(type: typeof(GetAttendance), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetByDate(string sdate, string edate)
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

            var entity = (from c in _officeAttendanceRepository.Table 
                                  join st in _staffRepository.Table on c.Staff equals st.StaffPersonalInfoId
                                  where c.Date >= startDate && c.Date <= stopDate
                                  select new GetAttendance
                                  {
                                      AttendanceId = c.AttendanceId,
                                      Date = c.Date,
                                      OfficeLocation = (from t in st.StaffOfficeLocation
                                                        join ol in _locationRepository.Table on t.Id equals ol.OfficeLocationId
                                                        where ol.OfficeLocationId == t.Id
                                                        select new GetStaffOfficeLocation
                                                        {
                                                            Id = t.Id,
                                                            Location = t.Location,
                                                            Staff = t.Staff,
                                                            LocationName = ol.Address
                                                        }).ToList(),
                                      JobTitle = c.JobTitle,
                                      StaffName = string.Concat(st.FirstName, " ", st.MiddleName, " ", st.LastName),
                                      Location = c.Location,
                                      ClockInAddress = c.ClockInAddress,
                                      ClockInDistance = c.ClockInDistance,
                                      ClockOutAddress = c.ClockOutAddress,
                                      ClockOutDistance = c.ClockOutDistance,
                                      StartTime = c.StartTime,
                                      StopTime = c.StopTime,
                                      ClockIn = c.ClockIn,
                                      ClockOut = c.ClockOut,
                                      ClockInMethod = c.ClockInMethod,
                                      ClockOutMethod = c.ClockOutMethod,
                                      ClockDiff = c.ClockDiff,
                                      Remark = c.Remark
                                  }
                      ).OrderBy(o => o.Date).ToList();

            return Ok(entity);
        }
        #endregion
    }
}
