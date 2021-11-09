using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.TrackingConcernNote;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TrackingConcernNoteController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<TrackingConcernNote> _trackingConcernNoteRepository;
        private IGenericRepository<TrackingConcernManager> _trackingConcernNoteManagerRepository;
        private IGenericRepository<TrackingConcernStaff> _trackingConcernNoteStaffRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;

        public TrackingConcernNoteController(AwesomeCareDbContext dbContext, IGenericRepository<TrackingConcernNote> TrackingConcernNoteRepository, IGenericRepository<StaffPersonalInfo> staffRepository,
            IGenericRepository<TrackingConcernManager> TrackingConcernNoteManagerRepository, IGenericRepository<TrackingConcernStaff> TrackingConcernNoteStaffRepository)
        {

            _dbContext = dbContext;
            _staffRepository = staffRepository;
            _trackingConcernNoteRepository = TrackingConcernNoteRepository;
            _trackingConcernNoteManagerRepository = TrackingConcernNoteManagerRepository;
            _trackingConcernNoteStaffRepository = TrackingConcernNoteStaffRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetTrackingConcernNote>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _trackingConcernNoteRepository.Table.ToList();
            return Ok(getEntities);
        }
        [HttpGet()]
        [Route("[action]")]
        [ProducesResponseType(type: typeof(List<GetTrackingConcernNote>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetWithChild()
        {
            var getentity = (from c in _trackingConcernNoteRepository.Table
                             select new GetTrackingConcernNote
                             {
                                 Ref = c.Ref,
                                 ConcernNote = c.ConcernNote,
                                 Date = c.Date,
                                 ActionRequired = c.ActionRequired,
                                 DateOfIncident = c.DateOfIncident,
                                 ExpectedDeadline = c.ExpectedDeadline,
                                 StaffNotify = c.StaffNotify,
                                 ManagerCopied = c.ManagerCopied,
                                 Remarks = c.Remarks,
                                 Status = c.Status,
                                 Attachment = c.Attachment,

                                 GetManagerInvolved = (from com in _trackingConcernNoteManagerRepository.Table
                                                join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                where com.TrackingConcernNoteId == c.Ref
                                                select new GetTrackingConcernManager
                                                {
                                                    StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                    StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                }).ToList(),
                                 GetStaffInvolved = (from com in _trackingConcernNoteStaffRepository.Table
                                                      join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                      where com.TrackingConcernNoteId == c.Ref
                                                      select new GetTrackingConcernStaff
                                                      {
                                                          StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                          StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                      }).ToList()
                             }
                      ).ToList();
            return Ok(getentity);
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostTrackingConcernNote post)
        {
            if (post == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var TrackingConcernNote = Mapper.Map<TrackingConcernNote>(post);
            await _trackingConcernNoteRepository.InsertEntity(TrackingConcernNote);
            return Ok();
        }

        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutTrackingConcernNote models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.PutManagerInvolved.ToList())
            {
                var entity = _dbContext.Set<TrackingConcernManager>();
                var filterentity = entity.Where(c => c.TrackingConcernNoteId == model.TrackingConcernNoteId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            foreach (var model in models.PutStaffInvolved.ToList())
            {
                var entity = _dbContext.Set<TrackingConcernStaff>();
                var filterentity = entity.Where(c => c.TrackingConcernNoteId == model.TrackingConcernNoteId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            _dbContext.SaveChanges();
            var TrackingConcernNote = Mapper.Map<TrackingConcernNote>(models);
            await _trackingConcernNoteRepository.UpdateEntity(TrackingConcernNote);
            return Ok();

        }

        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetTrackingConcernNote), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getentity = await (from c in _trackingConcernNoteRepository.Table
                                   where c.Ref == id.Value
                                   select new GetTrackingConcernNote
                                   {
                                       Ref = c.Ref,
                                       ConcernNote = c.ConcernNote,
                                       Date = c.Date,
                                       ActionRequired = c.ActionRequired,
                                       DateOfIncident = c.DateOfIncident,
                                       ExpectedDeadline = c.ExpectedDeadline,
                                       StaffNotify = c.StaffNotify,
                                       ManagerCopied = c.ManagerCopied,
                                       Remarks = c.Remarks,
                                       Status = c.Status,
                                       Attachment = c.Attachment,

                                       GetManagerInvolved = (from com in _trackingConcernNoteManagerRepository.Table
                                                             join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                             where com.TrackingConcernNoteId == c.Ref
                                                             select new GetTrackingConcernManager
                                                             {
                                                                 StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                 StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                             }).ToList(),
                                       GetStaffInvolved = (from com in _trackingConcernNoteStaffRepository.Table
                                                           join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                           where com.TrackingConcernNoteId == c.Ref
                                                           select new GetTrackingConcernStaff
                                                           {
                                                               StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                               StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                           }).ToList()
                                   }
                      ).FirstOrDefaultAsync();
            return Ok(getentity);
        }
    }
}
