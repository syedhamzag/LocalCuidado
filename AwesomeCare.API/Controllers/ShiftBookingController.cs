﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ShiftBooking;
using AwesomeCare.DataTransferObject.DTOs.StaffShiftBooking;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShiftBookingController : ControllerBase
    {
        private IGenericRepository<ShiftBooking> _shiftBookingRepository;
        private IGenericRepository<StaffPersonalInfo> _staffPersonalInfoRepository;
        private IGenericRepository<Rota> _rota;
        private IGenericRepository<StaffWorkTeam> _staffWorkTeamRepo;
        private IGenericRepository<StaffShiftBooking> _staffShiftBookingRepo;
        private IGenericRepository<StaffShiftBookingDay> _staffShiftBookingDayRepo;
        private AwesomeCareDbContext _dbContext;
        private ILogger<ShiftBookingController> _logger;

        public ShiftBookingController(IGenericRepository<StaffShiftBooking> staffShiftBookingRepo,
            IGenericRepository<StaffWorkTeam> staffWorkTeamRepo,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
            IGenericRepository<Rota> rota, ILogger<ShiftBookingController> logger,
            IGenericRepository<ShiftBooking> shiftBookingRepository,
            AwesomeCareDbContext dbContext,
            IGenericRepository<StaffShiftBookingDay> staffShiftBookingDayRepo)
        {
            _shiftBookingRepository = shiftBookingRepository;
            _dbContext = dbContext;
            _logger = logger;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
            _rota = rota;
            _staffWorkTeamRepo = staffWorkTeamRepo;
            _staffShiftBookingRepo = staffShiftBookingRepo;
            _staffShiftBookingDayRepo = staffShiftBookingDayRepo;
        }


        [HttpGet("{id}", Name = "GetShiftBookingById")]
        [ProducesResponseType(type: typeof(GetShiftBooking), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = (from shift in _shiftBookingRepository.Table
                             join staff in _staffPersonalInfoRepository.Table on shift.Team equals staff.StaffPersonalInfoId
                             join rt in _rota.Table on shift.Rota equals rt.RotaId
                             join wt in _staffWorkTeamRepo.Table on shift.PublishTo equals wt.StaffWorkTeamId
                             where shift.ShiftBookingId == id
                             select new GetShiftBookingDetails
                             {
                                 Team = shift.Team,
                                 DriverRequired = shift.DriverRequired ? "Yes" : "No",
                                 NumberOfStaff = shift.NumberOfStaff,
                                 PublishTo = shift.PublishTo,
                                 Remark = shift.Remark,
                                 Rota = shift.Rota,
                                 ShiftBookingId = shift.ShiftBookingId,
                                 ShiftDate = shift.ShiftDate,
                                 StartTime = shift.StartTime,
                                 StopTime = shift.StopTime,
                                 TeamStaff = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName),
                                 RotaName = rt.RotaName,
                                 PublishToWorkTeam = wt.WorkTeam
                             }).FirstOrDefault();

            return Ok(getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetShiftBookingDetails>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {

            var getEntities = (from shift in _shiftBookingRepository.Table
                               join staff in _staffPersonalInfoRepository.Table on shift.Team equals staff.StaffPersonalInfoId
                               join rt in _rota.Table on shift.Rota equals rt.RotaId
                               join wt in _staffWorkTeamRepo.Table on shift.PublishTo equals wt.StaffWorkTeamId
                               select new GetShiftBookingDetails
                               {
                                   Team = shift.Team,
                                   DriverRequired = shift.DriverRequired ? "Yes" : "No",
                                   NumberOfStaff = shift.NumberOfStaff,
                                   PublishTo = shift.PublishTo,
                                   Remark = shift.Remark,
                                   Rota = shift.Rota,
                                   ShiftBookingId = shift.ShiftBookingId,
                                   ShiftDate = shift.ShiftDate,
                                   StartTime = shift.StartTime,
                                   StopTime = shift.StopTime,
                                   TeamStaff = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName),
                                   RotaName = rt.RotaName,
                                   PublishToWorkTeam = wt.WorkTeam
                               }).ToList();

            return Ok(getEntities);
        }

        /// <summary>
        /// Create Shift Booking
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetShiftBooking), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PostShiftBooking model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<ShiftBooking>(model);
            var newEntity = await _shiftBookingRepository.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetShiftBooking>(newEntity);

            return CreatedAtRoute("GetShiftBookingById", new { id = getEntity.ShiftBookingId }, getEntity);
        }

        /// <summary>
        /// Create Staff Booking
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Staff/CreateBooking")]
        [HttpPost]
        [ProducesResponseType(type: typeof(GetStaffShiftBooking), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStaffBooking([FromBody] PostStaffShiftBooking model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffShiftBooking>(model);
            var newEntity = await _staffShiftBookingRepo.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetStaffShiftBooking>(newEntity);
            return Ok(getEntity);
            // return CreatedAtRoute("GetShiftBookingById", new { id = getEntity.ShiftBookingId }, getEntity);
        }

        [HttpGet("{month}/{year}", Name = "GetShiftBookingByMonthYear")]
        [ProducesResponseType(type: typeof(GetShiftBookedByMonthYear), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetShiftBookingByMonthYear(string month, string year)
        {
            string monthyear = $"{month}/{year}";
            var entity = await (from shift in _shiftBookingRepository.Table
                                where shift.ShiftDate == monthyear
                                // join staffShiftBooking in _stafShiftBookingRepo.Table on shift.ShiftBookingId equals staffShiftBooking.ShiftBookingId
                                select new GetShiftBookedByMonthYear
                                {
                                    TeamId = shift.Team,
                                    DriverRequired = shift.DriverRequired,
                                    NumberOfStaffRequired = shift.NumberOfStaff,
                                    PublishTo = shift.PublishTo,
                                    Remark = shift.Remark,
                                    RotaId = shift.Rota,
                                    ShiftBookingId = shift.ShiftBookingId,
                                    ShiftDate = shift.ShiftDate,
                                    StartTime = shift.StartTime,
                                    StopTime = shift.StopTime,
                                    NumberOfStaffRegistered = shift.StaffShiftBooking.Count,
                                    BookedDays = (from shiftStaff in shift.StaffShiftBooking
                                                  join shiftDay in _staffShiftBookingDayRepo.Table on shiftStaff.StaffShiftBookingId equals shiftDay.StaffShiftBookingId
                                                  select new BookedDays
                                                  {
                                                      Day = shiftDay.Day,
                                                      ShiftBookedById = shiftStaff.StaffPersonalInfoId,
                                                      StaffShiftBookingDayId = shiftDay.StaffShiftBookingDayId,
                                                      StaffShiftBookingId = shiftDay.StaffShiftBookingId,
                                                      WeekDay = shiftDay.WeekDay
                                                  }).ToList()
                                }).FirstOrDefaultAsync();

            return Ok(entity);

        }

        [AllowAnonymous]
        [HttpGet("Admin/{monthId}", Name = "GetShiftForAdminByMonth")]
        [ProducesResponseType(type: typeof(GetShiftBookedByMonthYear), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetShiftForAdminByMonth(int monthId)
        {
            string year = DateTime.Now.Year.ToString();
            string month = monthId.ToString("D2");
            string monthyear = $"{month}/{year}";
            var entity = await (from shift in _shiftBookingRepository.Table
                                //from sb in _shiftBookingRepository.Table
                                where shift.ShiftDate == monthyear
                                select new GetShiftBookedByMonthYear
                                {
                                    TeamId = shift.Team,
                                    DriverRequired = shift.DriverRequired,
                                    NumberOfStaffRequired = shift.NumberOfStaff,
                                    PublishTo = shift.PublishTo,
                                    Remark = shift.Remark,
                                    RotaId = shift.Rota,
                                    ShiftBookingId = shift.ShiftBookingId,
                                    ShiftDate = shift.ShiftDate,
                                    StartTime = shift.StartTime,
                                    StopTime = shift.StopTime,
                                    NumberOfStaffRegistered = shift.StaffShiftBooking.Count,
                                    BookedDays = (from shiftStaff in shift.StaffShiftBooking
                                                  join shiftDay in _staffShiftBookingDayRepo.Table on shiftStaff.StaffShiftBookingId equals shiftDay.StaffShiftBookingId
                                                 // join staff in _staffPersonalInfoRepository.Table on shiftStaff.StaffPersonalInfoId equals staff.StaffPersonalInfoId

                                                  select new BookedDays
                                                  {
                                                      Day = shiftDay.Day,
                                                      ShiftBookedById = shiftStaff.StaffPersonalInfoId,
                                                      StaffShiftBookingDayId = shiftDay.StaffShiftBookingDayId,
                                                      StaffShiftBookingId = shiftDay.StaffShiftBookingId,
                                                      WeekDay = shiftDay.WeekDay,
                                                      Staffs = (from booking in staff
                                                              //  join staff in _staffPersonalInfoRepository.Table on booking.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                                //  where booking.ShiftBookingId == shift.ShiftBookingId
                                                                select new StaffBooked
                                                                {
                                                                    StaffPersonalInfoId = booking.StaffPersonalInfoId,
                                                                    StaffName = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName,
                                                                    IsStaffDriver = staff.CanDrive == "Yes"
                                                                }).ToList()
                                                  }).ToList()
                                }).FirstOrDefaultAsync();
            return Ok(entity);
        }
    }
}