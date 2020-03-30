using System;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IGenericRepository<StaffShiftBooking> _stafShiftBookingRepo;
        private AwesomeCareDbContext _dbContext;
        private ILogger<ShiftBookingController> _logger;

        public ShiftBookingController(IGenericRepository<StaffShiftBooking> stafShiftBookingRepo,IGenericRepository<StaffWorkTeam> staffWorkTeamRepo, IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository, IGenericRepository<Rota> rota, ILogger<ShiftBookingController> logger, IGenericRepository<ShiftBooking> shiftBookingRepository, AwesomeCareDbContext dbContext)
        {
            _shiftBookingRepository = shiftBookingRepository;
            _dbContext = dbContext;
            _logger = logger;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
            _rota = rota;
            _staffWorkTeamRepo = staffWorkTeamRepo;
            _stafShiftBookingRepo = stafShiftBookingRepo;
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
                                   DriverRequired = shift.DriverRequired?"Yes":"No",
                                   NumberOfStaff = shift.NumberOfStaff,
                                   PublishTo = shift.PublishTo,
                                   Remark = shift.Remark,
                                   Rota = shift.Rota,
                                   ShiftBookingId = shift.ShiftBookingId,
                                   ShiftDate= shift.ShiftDate,
                                   StartTime=shift.StartTime,
                                   StopTime = shift.StopTime,
                                   TeamStaff = string.Concat(staff.FirstName," ",staff.MiddleName," ",staff.LastName),
                                   RotaName= rt.RotaName,
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
        public async Task<IActionResult> Post([FromBody]PostShiftBooking model)
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
        public async Task<IActionResult> CreateStaffBooking([FromBody]PostStaffShiftBooking model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffShiftBooking>(model);
            var newEntity = await _stafShiftBookingRepo.InsertEntity(postEntity);
            var getEntity = Mapper.Map<GetStaffShiftBooking>(newEntity);
            return Ok(getEntity);
           // return CreatedAtRoute("GetShiftBookingById", new { id = getEntity.ShiftBookingId }, getEntity);
        }

    }
}