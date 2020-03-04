using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.Enums;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StaffInfoController : ControllerBase
    {
        private IGenericRepository<StaffPersonalInfo> _staffInfoRepository;
        private ILogger<StaffInfoController> _logger;
        private IDbContext _dbContext;
        
        public StaffInfoController(IGenericRepository<StaffPersonalInfo> staffInfoRepository, ILogger<StaffInfoController> logger, IDbContext dbContext)
        {
            _staffInfoRepository = staffInfoRepository;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}", Name = "GetStaffById")]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int? id)
        {

            var entity = await _staffInfoRepository.GetEntity(id);
            var getEntity = Mapper.Map<GetStaffPersonalInfo>(entity);
            return Ok(getEntity);
        }
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetStaffPersonalInfo>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {

            var entity = await _staffInfoRepository.GetEntities();
            var getEntity = Mapper.Map<List<GetStaffPersonalInfo>>(entity);
            return Ok(getEntity);
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody]PostStaffPersonalInfo model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEmailRegistered = _staffInfoRepository.Table.Any(s => s.Email.Equals(model.Email));
            if (isEmailRegistered)
            {
                ModelState.AddModelError("Email", $"Email {model.Email} is registered");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffPersonalInfo>(model);
            var entity = await _staffInfoRepository.InsertEntity(postEntity);
            entity.RegistrationId = $"AHS/ST/{DateTime.Now.ToString("yy")}/{ entity.StaffPersonalInfoId.ToString("D6")}";
            entity = await _staffInfoRepository.UpdateEntity(entity);

            return CreatedAtAction("GetAsync", new { id = entity.StaffPersonalInfoId }, entity);


        }

        [Route("PostStaffFullInfo")]
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostStaffFullInfo([FromBody]PostStaffFullInfo model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool isEmailRegistered = _staffInfoRepository.Table.Any(s => s.Email.Equals(model.Email));
            if (isEmailRegistered)
            {
                ModelState.AddModelError("Email", $"Email {model.Email} is registered");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postEntity = Mapper.Map<StaffPersonalInfo>(model);
            var entity = await _staffInfoRepository.InsertEntity(postEntity);
            entity.RegistrationId = $"AHS/ST/{DateTime.Now.ToString("yy")}/{ entity.StaffPersonalInfoId.ToString("D6")}";
            entity = await _staffInfoRepository.UpdateEntity(entity);

            return Ok(entity.StaffPersonalInfoId);

        }


        [HttpGet("Profile/{id}")]
        [ProducesResponseType(type: typeof(GetStaffProfile), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Profile(int id)
        {
            //var staffDetails = await _staffInfoRepository.Table
            //    .Include(c => c.Education)
            //    .Include(c => c.References)
            //    .Include(c => c.Trainings)
            //    .FirstOrDefaultAsync(s => s.StaffPersonalInfoId == id);

            var baseRecordEntity = _dbContext.Set<BaseRecordModel>();
            var baseRecordItemEntity = _dbContext.Set<BaseRecordItemModel>();

           // var profile = Mapper.Map<GetStaffProfile>(staffDetails);

            var staffProfile = (from st in _staffInfoRepository.Table
                                where st.StaffPersonalInfoId == id
                                select new GetStaffProfile
                                {
                                    AboutMe = st.AboutMe,
                                    StaffPersonalInfoId = st.StaffPersonalInfoId,
                                    Address = st.Address,
                                    CanDrive = st.CanDrive,
                                    CoverLetter = st.CoverLetter,
                                    CV = st.CV,
                                    DateOfBirth = st.DateOfBirth,
                                    DBS = st.DBS,
                                    DBSAttachment = st.DBSAttachment,
                                    DBSExpiryDate = st.DBSExpiryDate,
                                    DBSUpdateNo = st.DBSUpdateNo,
                                    DrivingLicense = st.DrivingLicense,
                                    DrivingLicenseExpiryDate = st.DrivingLicenseExpiryDate,
                                    Education = (from edu in st.Education
                                                 select new GetStaffEducation
                                                 {
                                                     Address = edu.Address,
                                                     Certificate = edu.Certificate,
                                                     CertificateAttachment = edu.CertificateAttachment,
                                                     EndDate = edu.EndDate,
                                                     Institution = edu.Institution,
                                                     Location = edu.Location,
                                                     StaffEducationId = edu.StaffEducationId,
                                                     StaffPersonalInfoId = edu.StaffPersonalInfoId,
                                                     StartDate = edu.StartDate
                                                 }).ToList(),
                                    Email = st.Email,
                                    StartDate = st.StartDate,
                                    EndDate = st.EndDate,
                                    FirstName = st.FirstName,
                                    Gender = st.Gender,
                                    Hobbies = st.Hobbies,
                                    IdNumber = st.IdNumber,
                                    Keyworker = st.Keyworker,
                                    LastName = st.LastName,
                                    MiddleName = st.MiddleName,
                                    NI = st.NI,
                                    NIAttachment = st.NIAttachment,
                                    NIExpiryDate = st.NIExpiryDate,
                                    Passcode = st.Passcode,
                                    PostCode = st.PostCode,
                                    ProfilePix = st.ProfilePix,
                                    Rate = st.Rate,
                                    References = (from rf in st.References
                                                  select new GetStaffReferee
                                                  {
                                                      Address = rf.Address,
                                                      Attachment = rf.Attachment,
                                                      CompanyName = rf.CompanyName,
                                                      Email = rf.Email,
                                                      PhoneNumber = rf.PhoneNumber,
                                                      PositionofReferee = rf.PositionofReferee,
                                                      Referee = rf.Referee,
                                                      StaffPersonalInfoId = rf.StaffPersonalInfoId,
                                                      StaffRefereeId = rf.StaffRefereeId
                                                  }).ToList(),
                                    RegistrationId = st.RegistrationId,
                                    RightToWork = st.RightToWork,
                                    RightToWorkAttachment = st.RightToWorkAttachment,
                                    RightToWorkExpiryDate = st.RightToWorkExpiryDate,
                                    Self_PYE = st.Self_PYE,
                                    Self_PYEAttachment = st.Self_PYEAttachment,
                                    Status = st.Status.ToString(),
                                    TeamLeader = st.TeamLeader,
                                    Telephone = st.Telephone,
                                    Trainings = (from t in st.Trainings
                                                 select new GetStaffTraining
                                                 {
                                                     StaffPersonalInfoId = t.StaffPersonalInfoId,
                                                     Certificate = t.Certificate,
                                                     CertificateAttachment = t.CertificateAttachment,
                                                     ExpiredDate = t.ExpiredDate,
                                                     Location = t.Location,
                                                     StaffTrainingId = t.StaffTrainingId,
                                                     StartDate = t.StartDate,
                                                     Trainer = t.Trainer,
                                                     Training = t.Training
                                                 }).ToList(),
                                    WorkTeam = st.WorkTeam,
                                    RegulatoryContacts = (from rc in st.RegulatoryContact
                                                          join bitem in baseRecordItemEntity on rc.BaseRecordItemId equals bitem.BaseRecordItemId
                                                          join baseRec in baseRecordEntity on bitem.BaseRecordId equals baseRec.BaseRecordId
                                                          select new GetStaffRegulatoryContact { 
                                                          BaseRecordItemId = bitem.BaseRecordItemId,
                                                          DatePerformed =rc.DatePerformed,
                                                          DueDate=rc.DueDate,
                                                          Evidence=rc.Evidence,
                                                          RegulatoryContact=bitem.ValueName,
                                                          StaffPersonalInfoId =rc.StaffPersonalInfoId,
                                                          StaffRegulatoryContactId=rc.StaffRegulatoryContactId
                                                          }).ToList()
                                }).FirstOrDefault();

            return Ok(staffProfile);
        }

        /// <summary>
        /// Get staffs with few properties
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStaffs")]
        [ProducesResponseType(type: typeof(List<GetStaffs>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStaffs()
        {
            var staffs = await _staffInfoRepository.Table.ProjectTo<GetStaffs>().ToListAsync();
            return Ok(staffs);
        }


        [HttpPost("Approval")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Approval([FromBody]PostStaffApproval model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var staffPersonalInfo = _dbContext.Set<StaffPersonalInfo>();
            var staff = staffPersonalInfo.FirstOrDefault(s => s.StaffPersonalInfoId == model.StaffPersonalInfoId);
            if (staff == null)
                return NotFound();

            staff.Rate = model.Rate;
            staff.Status = (int)Enum.Parse(typeof(StaffRegistrationEnum), model.Status);
            staff.StaffPersonalInfoComments.Add(new StaffPersonalInfoComment
            {
                Comment = model.Comment,
                CommentBy_UserId = model.CommentBy_UserId,
                CommentOn = DateTime.Now,
                StaffPersonalInfoId = model.StaffPersonalInfoId
            });
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}