using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Staff;
using AwesomeCare.DataTransferObject.DTOs.StaffAdlObs;
using AwesomeCare.DataTransferObject.DTOs.StaffKeyWorker;
using AwesomeCare.DataTransferObject.DTOs.StaffMedComp;
using AwesomeCare.DataTransferObject.DTOs.StaffOneToOne;
using AwesomeCare.DataTransferObject.DTOs.StaffRating;
using AwesomeCare.DataTransferObject.DTOs.StaffReference;
using AwesomeCare.DataTransferObject.DTOs.StaffRota;
using AwesomeCare.DataTransferObject.DTOs.StaffSpotCheck;
using AwesomeCare.DataTransferObject.DTOs.StaffSupervision;
using AwesomeCare.DataTransferObject.DTOs.StaffSurvey;
using AwesomeCare.DataTransferObject.Enums;
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
    public class StaffInfoController : ControllerBase
    {
        private IGenericRepository<StaffPersonalInfo> _staffInfoRepository;
        private IGenericRepository<StaffRotaPeriod> _staffRotaPeriodRepository;
        private IGenericRepository<StaffRotaPartner> _staffRotaPartnerRepository;
        private IGenericRepository<StaffRota> _staffRotaRepository;
        private IGenericRepository<StaffRotaDynamicAddition> _staffDynamicAdditionRepository;
        private IGenericRepository<ClientRotaType> _clientRotaTypeRepository;
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<StaffAdlObs> _staffAdlObs;
        private IGenericRepository<StaffSpotCheck> _staffspotcheck;
        private IGenericRepository<StaffMedComp> _staffmedcomp;
        private IGenericRepository<StaffKeyWorkerVoice> _staffkeyworker;
        private IGenericRepository<StaffSurvey> _staffsurvey;
        private IGenericRepository<StaffOneToOne> _staffonetoone;
        private IGenericRepository<StaffSupervisionAppraisal> _staffsupervision;
        private IGenericRepository<StaffReference> _staffreference;
        private readonly IGenericRepository<ApplicationUser> applicationUserRepository;
        private readonly IGenericRepository<StaffWorkTeam> staffWorkTeamRepository;
        private readonly IGenericRepository<ClientRota> clientRotaRepository;
        private readonly IGenericRepository<ClientRotaDays> clientRotaDaysRepository;
        private IGenericRepository<StaffRating> _staffRatingRepository;
        private ILogger<StaffInfoController> _logger;
        private AwesomeCareDbContext _dbContext;

        public StaffInfoController(IGenericRepository<StaffPersonalInfo> staffInfoRepository,
            IGenericRepository<StaffRotaDynamicAddition> staffDynamicAdditionRepository,
            IGenericRepository<StaffRota> staffRotaRepository, ILogger<StaffInfoController> logger,
            AwesomeCareDbContext dbContext, IGenericRepository<StaffRotaPartner> staffRotaPartnerRepository,
            IGenericRepository<StaffRotaPeriod> staffRotaPeriodRepository, IGenericRepository<ClientRotaType> clientRotaTypeRepository,
            IGenericRepository<StaffRating> staffRatingRepository,
            IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffWorkTeam> staffWorkTeamRepository,
            IGenericRepository<ClientRota> clientRotaRepository,
            IGenericRepository<ClientRotaDays> clientRotaDaysRepository,
            IGenericRepository<ApplicationUser> applicationUserRepository,
            IGenericRepository<StaffAdlObs> staffAdlObs,
            IGenericRepository<StaffSpotCheck> staffspotcheck,
            IGenericRepository<StaffMedComp> staffmedcomp,
            IGenericRepository<StaffKeyWorkerVoice> staffkeyworker,
            IGenericRepository<StaffSurvey> staffsurvey,
            IGenericRepository<StaffOneToOne> staffonetoone,
            IGenericRepository<StaffSupervisionAppraisal> staffsupervision,
            IGenericRepository<StaffReference> staffreference)
        {
            _staffInfoRepository = staffInfoRepository;
            _logger = logger;
            _dbContext = dbContext;
            _staffRotaRepository = staffRotaRepository;
            _staffDynamicAdditionRepository = staffDynamicAdditionRepository;
            _staffRotaPartnerRepository = staffRotaPartnerRepository;
            _staffRotaPeriodRepository = staffRotaPeriodRepository;
            _clientRotaTypeRepository = clientRotaTypeRepository;
            _staffRatingRepository = staffRatingRepository;
            _clientRepository = clientRepository;
            this.applicationUserRepository = applicationUserRepository;
            this.staffWorkTeamRepository = staffWorkTeamRepository;
            this.clientRotaRepository = clientRotaRepository;
            this.clientRotaDaysRepository = clientRotaDaysRepository;
            _staffAdlObs = staffAdlObs;
            _staffspotcheck = staffspotcheck;
            _staffmedcomp = staffmedcomp;
            _staffsurvey = staffsurvey;
            _staffsupervision = staffsupervision;
            _staffonetoone = staffonetoone;
            _staffreference = staffreference;
            _staffkeyworker = staffkeyworker;
        }

        [HttpGet("{id}", Name = "GetStaffById")]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int? id)
        {

            // var entity = await _staffInfoRepository.GetEntity(id);
            var getEntity = await _staffInfoRepository.Table.ProjectTo<GetStaffPersonalInfo>().FirstOrDefaultAsync(s => s.StaffPersonalInfoId == id);
            // var  = Mapper.Map<GetStaffPersonalInfo>(entity);
            return Ok(getEntity);
        }


        [HttpGet("GetByApplicationUserId/{userId}", Name = "GetByApplicationUserId")]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByApplicationUserIdAsync(string userId)
        {

            var entity = await _staffInfoRepository.Table.FirstOrDefaultAsync(u => u.ApplicationUserId == userId);
            if (entity == null)
                return NotFound();

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
        public async Task<IActionResult> PostAsync([FromBody] PostStaffPersonalInfo model)
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
        public async Task<IActionResult> PostStaffFullInfo([FromBody] PostStaffFullInfo model)
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

            //var baseRecordEntity = _dbContext.Set<BaseRecordModel>();
            //var baseRecordItemEntity = _dbContext.Set<BaseRecordItemModel>();
            // var teamEntity = _dbContext.Set<StaffWorkTeam>();
            // var profile = Mapper.Map<GetStaffProfile>(staffDetails);

            var staffProfile = (from st in _staffInfoRepository.Table
                                join wt in staffWorkTeamRepository.Table on st.WorkTeam equals wt.StaffWorkTeamId.ToString() into workTeamGroup
                                from wk in workTeamGroup.DefaultIfEmpty()
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
                                    EmploymentDate = st.EmploymentDate,
                                    HasIdCard = st.HasIdCard.Value ? "Yes" : "No",
                                    HasUniform = st.HasUniform.Value ? "Yes" : "No",
                                    IsTeamLeader = st.IsTeamLeader.Value ? "Yes" : "No",
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
                                    PlaceOfBirth = st.PlaceOfBirth,
                                    JobCategory = st.JobCategory,
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
                                    Status = st.Status,
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
                                    WorkTeam = wk == null ? string.Empty : wk.WorkTeam,
                                    GetStaffAdlObs = (from adl in _staffAdlObs.Table
                                                      where adl.StaffId == st.StaffPersonalInfoId
                                                      select new GetStaffAdlObs
                                                      {
                                                          ActionRequired = adl.ActionRequired,
                                                          Attachment = adl.Attachment
                                                      }).ToList(),
                                    GetStaffMedComp = (from s in _staffmedcomp.Table
                                                       where s.StaffId == st.StaffPersonalInfoId
                                                       select new GetStaffMedComp
                                                       {
                                                           ActionRequired = s.ActionRequired,
                                                           Attachment = s.Attachment
                                                       }).ToList(),
                                    GetStaffKeyWorkerVoice = (from s in st.StaffKeyWorkerVoice
                                                              where s.StaffId == st.StaffPersonalInfoId
                                                              select new GetStaffKeyWorkerVoice
                                                              {
                                                                  ActionRequired = s.ActionRequired,
                                                                  Attachment = s.Attachment
                                                              }).ToList(),
                                    GetStaffOneToOne = (from s in _staffonetoone.Table
                                                        where s.StaffId == st.StaffPersonalInfoId
                                                        select new GetStaffOneToOne
                                                        {
                                                            ActionRequired = s.ActionRequired,
                                                            Attachment = s.Attachment
                                                        }).ToList(),
                                    GetStaffReference = (from s in _staffreference.Table
                                                         where s.StaffId == st.StaffPersonalInfoId
                                                         select new GetStaffReference
                                                         {
                                                             Relationship = s.Relationship,
                                                             Attachment = s.Attachment
                                                         }).ToList(),
                                    GetStaffSpotCheck = (from s in _staffspotcheck.Table
                                                         where s.StaffId == st.StaffPersonalInfoId
                                                         select new GetStaffSpotCheck
                                                         {
                                                             ActionRequired = s.ActionRequired,
                                                             Attachment = s.Attachment
                                                         }).ToList(),
                                    GetStaffSupervisionAppraisal = (from s in _staffsupervision.Table
                                                                    where s.StaffId == st.StaffPersonalInfoId
                                                                    select new GetStaffSupervisionAppraisal
                                                                    {
                                                                        ActionRequired = s.ActionRequired,
                                                                        Attachment = s.Attachment
                                                                    }).ToList(),
                                    GetStaffSurvey = (from s in _staffsurvey.Table
                                                      where s.StaffId == st.StaffPersonalInfoId
                                                      select new GetStaffSurvey
                                                      {
                                                          ActionRequired = s.ActionRequired,
                                                          Attachment = s.Attachment
                                                      }).ToList(),
                                }).FirstOrDefault();

            return Ok(staffProfile);
        }

        [HttpGet("MyProfile")]
        [ProducesResponseType(type: typeof(GetStaffProfile), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> MyProfile()
        {

            var baseRecordEntity = _dbContext.Set<BaseRecordModel>();
            var baseRecordItemEntity = _dbContext.Set<BaseRecordItemModel>();

            var identityUserId = this.User.SubClaim();

            var staffProfile = (from st in _staffInfoRepository.Table
                                join wt in staffWorkTeamRepository.Table on st.WorkTeam equals wt.StaffWorkTeamId.ToString() into workTeamGroup
                                from wk in workTeamGroup.DefaultIfEmpty()
                                where st.ApplicationUserId == identityUserId
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
                                    EmploymentDate = st.EmploymentDate,
                                    HasIdCard = st.HasIdCard.Value ? "Yes" : "No",
                                    HasUniform = st.HasUniform.Value ? "Yes" : "No",
                                    IsTeamLeader = st.IsTeamLeader.Value ? "Yes" : "No",
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
                                    EmergencyContacts = (from em in st.EmergencyContacts
                                                         select new GetStaffEmergencyContact
                                                         {
                                                             Address = em.Address,
                                                             ContactName = em.ContactName,
                                                             Email = em.Email,
                                                             Relationship = em.Relationship,
                                                             StaffEmergencyContactId = em.StaffEmergencyContactId,
                                                             StaffPersonalInfoId = em.StaffPersonalInfoId,
                                                             Telephone = em.Telephone
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
                                    PlaceOfBirth = st.PlaceOfBirth,
                                    JobCategory = st.JobCategory,
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
                                    Status = st.Status,
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
                                    WorkTeam = wk == null ? string.Empty : wk.WorkTeam,
                                    StaffWorkTeamId = wk == null ? 0 : wk.StaffWorkTeamId,
                                    RegulatoryContacts = (from rc in st.RegulatoryContact
                                                          join bitem in baseRecordItemEntity on rc.BaseRecordItemId equals bitem.BaseRecordItemId
                                                          join baseRec in baseRecordEntity on bitem.BaseRecordId equals baseRec.BaseRecordId
                                                          select new GetStaffRegulatoryContact
                                                          {
                                                              BaseRecordItemId = bitem.BaseRecordItemId,
                                                              DatePerformed = rc.DatePerformed,
                                                              DueDate = rc.DueDate,
                                                              Evidence = rc.Evidence,
                                                              RegulatoryContact = bitem.ValueName,
                                                              StaffPersonalInfoId = rc.StaffPersonalInfoId,
                                                              StaffRegulatoryContactId = rc.StaffRegulatoryContactId,
                                                              AddLink = bitem.AddLink,
                                                              HasGoogleForm = bitem.HasGoogleForm,
                                                              ViewLink = bitem.ViewLink
                                                          }).ToList()
                                }).FirstOrDefault();

            return Ok(staffProfile);
        }

        [HttpGet("MyProfile/PersonalInfo")]
        [ProducesResponseType(type: typeof(GetStaffPersonalInfo), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonalInfo()
        {
            var identityUserId = this.User.SubClaim();

            var staffProfile = await _staffInfoRepository.Table.ProjectTo<GetStaffPersonalInfo>().FirstOrDefaultAsync(s => s.ApplicationUserId == identityUserId); ;

            return Ok(staffProfile);

        }

        //[HttpGet("MyProfile/Education")]
        //[ProducesResponseType(type: typeof(GetStaffEducation), statusCode: StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetMyEducation()
        //{

        //    var identityUserId = this.User.SubClaim();

        //    var staffProfile = await _staffInfoRepository.Table.ProjectTo<GetStaffPersonalInfo>().FirstOrDefaultAsync(s => s.ApplicationUserId == identityUserId); ;

        //    return Ok(staffProfile);

        //}


        [HttpPut("MyProfile/Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePersonalInfo([FromBody] PutStaffPersonalInfo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var profile = Mapper.Map<StaffPersonalInfo>(model);

            #region EmergencyContact
            //foreach (var em in profile.EmergencyContacts)
            //{
            //    if(em.StaffEmergencyContactId == default(int))
            //    {
            //        _dbContext.Entry<StaffEmergencyContact>(em).State = EntityState.Added;                   
            //    }
            //}
            #endregion

            await _staffInfoRepository.UpdateEntity(profile);
            return Ok();
        }

        /// <summary>
        /// Get staffs with few properties
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStaffs")]
        [ProducesResponseType(type: typeof(List<GetStaffs>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStaffs()
        {
            //  var staffs = await _staffInfoRepository.Table.ProjectTo<GetStaffs>().ToListAsync();
            var staffs = await (from st in _staffInfoRepository.Table
                                select new GetStaffs
                                {
                                    Address = st.Address,
                                    ApplicationUserId = st.ApplicationUserId,
                                    Email = st.Email,
                                    EndDate = st.EndDate.ToString(),
                                    Fullname = string.Concat(st.FirstName, " ", st.MiddleName, " ", st.LastName),
                                    RegistrationId = st.RegistrationId,
                                    StaffPersonalInfoId = st.StaffPersonalInfoId,
                                    StartDate = st.StartDate.ToString(),
                                    Status = st.Status.ToString(),
                                    Telephone = st.Telephone,
                                    CanDrive = st.CanDrive == "Yes" ? true : false

                                }).ToListAsync();
            return Ok(staffs);
        }


        [HttpPost("Approval")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Approval([FromBody] PostStaffApproval model)
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
            staff.Status = model.Status;// (int)Enum.Parse(typeof(StaffRegistrationEnum), model.Status);
            staff.HasUniform = model.HasUniform;
            staff.IsTeamLeader = model.IsTeamLeader;
            staff.HasIdCard = model.HasIdCard;
            staff.EmploymentDate = model.EmploymentDate;

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


        #region Rota
        [HttpPost("Rota/Create")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateStaffRota([FromBody] List<PostStaffRota> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var postEntity = Mapper.Map<List<StaffRota>>(model);

             await _staffRotaRepository.InsertEntities(postEntity);


            return Ok();
        }

       

        [HttpGet("Rota/Get/{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStaffRota(int id)
        {
            return Ok();
        }

        [HttpPost("Rota/Dynamic")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateStaffRotaAddition([FromBody] PostStaffRotaDynamicAddition model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var isExisting = _staffDynamicAdditionRepository.Table.Any(e => e.ItemName.ToLower() == model.ItemName.ToLower() && !e.Deleted);
            if (isExisting)
            {
                ModelState.AddModelError("", $"Item Name {model.ItemName} is already existing");
                return BadRequest(model);
            }
            var entity = Mapper.Map<StaffRotaDynamicAddition>(model);
            var result = await _staffDynamicAdditionRepository.InsertEntity(entity);
            return Ok();
        }

        [HttpPut("Rota/Dynamic")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStaffRotaAddition([FromBody] PutStaffRotaDynamicAddition model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var entity = _staffDynamicAdditionRepository.Table.FirstOrDefault(e => e.StaffRotaDynamicAdditionId == model.StaffRotaDynamicAdditionId);
            if (entity == null) return NotFound();


            var result = Mapper.Map<PutStaffRotaDynamicAddition, StaffRotaDynamicAddition>(model, entity);
            await _staffDynamicAdditionRepository.UpdateEntity(result);
            return Ok();
        }


        [HttpGet("Rota/Dynamic")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(List<GetStaffRotaDynamicAddition>), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStaffRotaAddition()
        {
            var entities = _staffDynamicAdditionRepository.Table.Where(e => !e.Deleted).ToList();
            var records = Mapper.Map<List<GetStaffRotaDynamicAddition>>(entities);
            return Ok(records);
        }

        [HttpGet("Rota/Dynamic/{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(type: typeof(GetStaffRotaDynamicAddition), statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStaffRotaAddition(int id)
        {
            var entity = _staffDynamicAdditionRepository.Table.FirstOrDefault(e => !e.Deleted && e.StaffRotaDynamicAdditionId == id);
            if (entity == null) return NotFound();

            var records = Mapper.Map<GetStaffRotaDynamicAddition>(entity);
            return Ok(records);
        }

        /// <summary>
        /// Get Staff Rotas
        /// </summary>
        /// <param name="sDate">StartDate (yyyy-MM-dd)</param>
        /// <param name="eDate">EndDate (yyyy-MM-dd)</param>
        /// <returns></returns>
        [HttpGet("Rota/Get/{sDate}/{eDate}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRotaLive(string sDate, string eDate)
        {

            string format = "yyyy-MM-dd";
            bool isStartDateValid = DateTime.TryParseExact(sDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime startDate);
            bool isEndDateValid = DateTime.TryParseExact(eDate, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime endDate);
            if (isStartDateValid || isEndDateValid)
            {
                return BadRequest("Invalid Date format, Format is yyyy-MM-dd");
            }
            var staffRotas = await (from sr in _staffRotaRepository.Table
                                    join staff in _staffInfoRepository.Table on sr.Staff equals staff.StaffPersonalInfoId
                                    where sr.RotaDate >= startDate && sr.RotaDate <= endDate
                                    select new GetStaffRota
                                    {
                                        Staff = staff.FirstName + " " + staff.MiddleName + " " + staff.LastName,
                                        ReferenceNumber = sr.ReferenceNumber,
                                        Remark = sr.Remark,
                                        RotaDate = sr.RotaDate,
                                        RotaId = sr.RotaId,
                                        StaffId = sr.Staff,
                                        Partners = (from pt in sr.StaffRotaPartners
                                                    join partner in _staffInfoRepository.Table on pt.StaffId equals partner.StaffPersonalInfoId
                                                    select new GetStaffRotaPartner
                                                    {
                                                        Partner = partner.FirstName + " " + partner.MiddleName + " " + partner.LastName,
                                                        PartnerId = pt.StaffId,
                                                        StaffRotaId = pt.StaffRotaId,
                                                        StaffRotaPartnerId = pt.StaffRotaPartnerId,
                                                        Telephone = partner.Telephone
                                                    }).ToList(),
                                        Periods = (from p in sr.StaffRotaPeriods
                                                   join ct in _clientRotaTypeRepository.Table on p.ClientRotaTypeId equals ct.ClientRotaTypeId
                                                   select new GetStaffRotaPeriod
                                                   {
                                                       ClientRotaTypeId = p.ClientRotaTypeId,
                                                       RotaType = ct.RotaType,
                                                       StaffRotaId = p.StaffRotaId,
                                                       StaffRotaPeriodId = p.StaffRotaPeriodId
                                                   }).ToList()
                                    }).OrderBy(b => b.RotaDate).ToListAsync();


            return Ok(staffRotas);

        }

        #endregion

        #region Client Feedbacks i.e Staff Ratings
        /// <summary>
        /// Get Client Feedbacks by StaffPersonalInfo Id
        /// </summary>
        /// <param name="staffPersonalInfoId"></param>
        /// <returns></returns>
        [HttpGet("ClientFeedback/{staffPersonalInfoId}")]
        [ProducesResponseType(type: typeof(List<GetStaffRating>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientFeedback(int? staffPersonalInfoId)
        {
            if (!staffPersonalInfoId.HasValue)
            {
                return BadRequest("staffPersonalInfoId is required");
            }
            var feedbacks = await (from fb in _staffRatingRepository.Table
                                   join st in _staffInfoRepository.Table on fb.StaffPersonalInfoId equals st.StaffPersonalInfoId
                                   join cl in _clientRepository.Table on fb.ClientId equals cl.ClientId
                                   where fb.StaffPersonalInfoId == staffPersonalInfoId.Value
                                   select new GetStaffRating
                                   {
                                       StaffPersonalInfoId = fb.StaffPersonalInfoId,
                                       Client = cl.Firstname + " " + cl.Middlename + " " + cl.Surname,
                                       ClientId = fb.ClientId,
                                       Comment = fb.Comment,
                                       CommentDate = fb.CommentDate,
                                       Rating = fb.Rating,
                                       Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                       StaffRatingId = fb.StaffRatingId
                                   }
                            ).ToListAsync();
            return Ok(feedbacks);
        }
        /// <summary>
        /// Post Client Feeedback for a staff
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ClientFeedback")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostClientFeedback([FromBody] PostStaffRating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var entity = Mapper.Map<StaffRating>(model);
            var staffRating = await _staffRatingRepository.InsertEntity(entity);
            return Ok();
        }
        #endregion
    }
}