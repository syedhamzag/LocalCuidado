using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Capacity;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentCare;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentData;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.ConsentLandline;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Equipment;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.KeyIndicators;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.PersonCentred;
using AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Review;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonalDetailController : ControllerBase
    {
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<PersonalDetail> _PersonalDetailRepository;
        private IGenericRepository<Capacity> _capacityRepository;
        private IGenericRepository<CapacityIndicator> _indicatorRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRepository;
        private IGenericRepository<ConsentCare> _consentCareRepository;
        private IGenericRepository<ConsentData> _consentDataRepository;
        private IGenericRepository<ConsentLandLine> _consentLandLineRepository;
        private IGenericRepository<Equipment> _equipmentRepository;
        private IGenericRepository<KeyIndicators> _keyIndicatorsRepository;
        private IGenericRepository<Personal> _personalRepository;
        private IGenericRepository<PersonCentred> _personCenteredRepository;
        private IGenericRepository<Review> _reviewRepository;
        private IGenericRepository<PersonCentredFocus> _focusRepository;
        private IGenericRepository<KeyIndicatorLog> _keylogRepository;
        private IGenericRepository<ConsentLandlineLog> _landlogRepository;

        public PersonalDetailController (IGenericRepository<PersonalDetail> PersonalDetailRepository, IGenericRepository<Capacity> capacityRepository,
            IGenericRepository<CapacityIndicator> indicatorRepository, IGenericRepository<BaseRecordItemModel> baseRepository, IGenericRepository<ConsentCare> consentCareRepository,
            IGenericRepository<ConsentData> consentDataRepository, IGenericRepository<ConsentLandLine> consentLandLineRepository,IGenericRepository<Equipment> equipmentRepository,
            IGenericRepository<KeyIndicators> keyIndicatorsRepository, IGenericRepository<Personal> personalRepository, IGenericRepository<PersonCentred> personCenteredRepository, 
            IGenericRepository<Review> reviewRepository, IGenericRepository<PersonCentredFocus> focusRepository, IGenericRepository<KeyIndicatorLog> keylogRepository,
            IGenericRepository<ConsentLandlineLog> landlogRepository,  AwesomeCareDbContext dbContext)
        {
            _PersonalDetailRepository = PersonalDetailRepository;
            _capacityRepository = capacityRepository;
            _indicatorRepository = indicatorRepository;
            _baseRepository = baseRepository;
            _consentCareRepository = consentCareRepository;
            _consentDataRepository = consentDataRepository;
            _consentLandLineRepository = consentLandLineRepository;
            _equipmentRepository = equipmentRepository;
            _keyIndicatorsRepository = keyIndicatorsRepository;
            _personalRepository = personalRepository;
            _personCenteredRepository = personCenteredRepository;
            _reviewRepository = reviewRepository;
            _focusRepository = focusRepository;
            _landlogRepository = landlogRepository;
            _keylogRepository = keylogRepository;
            _dbContext = dbContext;
        }
        #region PersonalDetail
        /// <summary>
        /// Get All PersonalDetail
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetPersonalDetail>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var entity = _PersonalDetailRepository.Table.ToList();
            if (entity == null) return NotFound();
            return Ok(entity);
        }
        /// <summary>
        /// Create PersonalDetail
        /// </summary>
        /// <param name="postPersonalDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostPersonalDetail postPersonalDetail)
        {
            if (postPersonalDetail == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PersonalDetail = Mapper.Map<PersonalDetail>(postPersonalDetail);
            await _PersonalDetailRepository.InsertEntity(PersonalDetail);
            return Ok();
        }
        /// <summary>
        /// Update PersonalDetail
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PostPersonalDetail models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var model in models.PersonCentred)
            {
                var entity = _dbContext.Set<PersonCentredFocus>();
                var filterentity = entity.Where(c => c.PersonCentredId == model.Focus.FirstOrDefault().PersonCentredId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            foreach (var model in models.Capacity.Indicator)
            {
                var entity = _dbContext.Set<CapacityIndicator>();
                var filterentity = entity.Where(c => c.CapacityId == model.CapacityId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            foreach (var model in models.ConsentLandline.LogMethod)
            {
                var entity = _dbContext.Set<ConsentLandlineLog>();
                var filterentity = entity.Where(c => c.LandlineId == model.LandlineId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            foreach (var model in models.KeyIndicators.LogMethod)
            {
                var entity = _dbContext.Set<KeyIndicatorLog>();
                var filterentity = entity.Where(c => c.KeyId == model.KeyId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }

            _dbContext.SaveChanges();
            var PersonalDetail = Mapper.Map<PersonalDetail>(models);
            await _PersonalDetailRepository.UpdateEntity(PersonalDetail);
            return Ok();

        }
        /// <summary>
        /// Get PersonalDetail by ProgramId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetPersonalDetail), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getPersonalDetail = await (from p in _PersonalDetailRepository.Table
                                     where p.ClientId== id.Value
                                     select new GetPersonalDetail
                                           {
                                               PersonalDetailId = p.PersonalDetailId,
                                               ClientId = p.ClientId,
                                               Capacity = (from c in _capacityRepository.Table
                                                           where c.PersonalDetailId == p.PersonalDetailId
                                                           select new GetCapacity
                                                           {
                                                               CapacityId = c.CapacityId,
                                                               PersonalDetailId = c.PersonalDetailId,
                                                               Implications = c.Implications,
                                                               Pointer = c.Pointer,
                                                               Indicator = (from com in _indicatorRepository.Table
                                                                            join b in _baseRepository.Table on com.BaseRecordId equals b.BaseRecordItemId
                                                                            where com.CapacityId == c.CapacityId
                                                                            select new GetCapacityIndicator
                                                                            {
                                                                                BaseRecordId = com.BaseRecordId,
                                                                                ValueName = b.ValueName
                                                                            }).ToList()
                                                           }).ToList(),
                                               ConsentCare = (from c in _consentCareRepository.Table
                                                              where c.PersonalDetailId == p.PersonalDetailId
                                                              select new GetConsentCare
                                                              {
                                                                  CareId = c.CareId,
                                                                  PersonalDetailId = c.PersonalDetailId,
                                                                  Date = c.Date,
                                                                  Name = c.Name,
                                                                  Signature = c.Signature,
                                                              }).ToList(),
                                               ConsentData = (from c in _consentDataRepository.Table
                                                              where c.PersonalDetailId == p.PersonalDetailId
                                                              select new GetConsentData
                                                              {
                                                                  DataId = c.DataId,
                                                                  PersonalDetailId = c.PersonalDetailId,
                                                                  Date = c.Date,
                                                                  Name = c.Name,
                                                                  Signature = c.Signature,
                                                              }).ToList(),
                                               ConsentLandline = (from c in _consentLandLineRepository.Table
                                                                  where c.PersonalDetailId == p.PersonalDetailId
                                                                  select new GetConsentLandLine
                                                                  {
                                                                      LandlineId = c.LandlineId,
                                                                      PersonalDetailId = c.PersonalDetailId,
                                                                      Date = c.Date,
                                                                      Name = c.Name,
                                                                      Signature = c.Signature,
                                                                      LogMethod = (from com in _landlogRepository.Table
                                                                                   join b in _baseRepository.Table on com.BaseRecordId equals b.BaseRecordItemId
                                                                                   where com.LandlineId == c.LandlineId
                                                                                   select new GetConsentLandlineLog
                                                                                   {
                                                                                       BaseRecordId = com.BaseRecordId,
                                                                                       ValueName = b.ValueName
                                                                                   }).ToList()
                                                                  }).ToList(),
                                               Equipment = (from c in _equipmentRepository.Table
                                                            where c.PersonalDetailId == p.PersonalDetailId
                                                            select new GetEquipment
                                                            {
                                                                EquipmentId = c.EquipmentId,
                                                                PersonalDetailId = c.PersonalDetailId,
                                                                Location = c.Location,
                                                                Type = c.Type,
                                                                Name = c.Name,
                                                                NextServiceDate = c.NextServiceDate,
                                                                ServiceDate = c.ServiceDate,
                                                                Attachment = c.Attachment,
                                                                PersonToAct = c.PersonToAct,
                                                                Status = c.Status
                                                            }).ToList(),
                                               KeyIndicators = (from c in _keyIndicatorsRepository.Table
                                                                where c.PersonalDetailId == p.PersonalDetailId
                                                                select new GetKeyIndicators
                                                                {
                                                                    KeyId = c.KeyId,
                                                                    PersonalDetailId = c.PersonalDetailId,
                                                                    AboutMe = c.AboutMe,
                                                                    FamilyRole = c.FamilyRole,
                                                                    Debture = c.Debture,
                                                                    LivingStatus = c.LivingStatus,
                                                                    ThingsILike = c.ThingsILike,
                                                                    LogMethod = (from com in _keylogRepository.Table
                                                                                 join b in _baseRepository.Table on com.BaseRecordId equals b.BaseRecordItemId
                                                                                 where com.KeyId == c.KeyId
                                                                                 select new GetKeyIndicatorLog
                                                                                 {
                                                                                     BaseRecordId = com.BaseRecordId,
                                                                                     ValueName = b.ValueName
                                                                                 }).ToList()
                                                                }).ToList(),
                                               Personal = (from c in _personalRepository.Table
                                                           where c.PersonalDetailId == p.PersonalDetailId
                                                           select new GetPersonal
                                                           {
                                                                DNR = c.DNR,
                                                                PersonalDetailId = c.PersonalDetailId,
                                                                PersonalId = c.PersonalId,
                                                                Nationality = c.Nationality,
                                                                Religion = c.Religion,
                                                                Smoking = c.Smoking
                                                           }).ToList(),
                                               PersonCentred = (from c in _personCenteredRepository.Table
                                                                where c.PersonalDetailId == p.PersonalDetailId
                                                                select new GetPersonCentred
                                                                {
                                                                    PersonCentredId = c.PersonCentredId,
                                                                    PersonalDetailId = c.PersonalDetailId,
                                                                    Class = c.Class,
                                                                    ExpSupport = c.ExpSupport,
                                                                    Focus = (from com in _focusRepository.Table
                                                                             join b in _baseRepository.Table on com.BaseRecordId equals b.BaseRecordItemId
                                                                             where com.PersonCentredId == c.PersonCentredId
                                                                             select new GetPersonCentredFocus
                                                                             {
                                                                                 BaseRecordId = com.BaseRecordId,
                                                                                 ValueName = b.ValueName
                                                                             }).ToList()
                                                                }).ToList(),
                                               Review = (from c in _reviewRepository.Table
                                                         where c.PersonalDetailId == p.PersonalDetailId
                                                         select new GetReview
                                                         {
                                                             ReviewId = c.ReviewId,
                                                             PersonalDetailId = c.PersonalDetailId,
                                                             CP_PreDate = c.CP_PreDate,
                                                             CP_ReviewDate = c.CP_ReviewDate,
                                                             RA_PreDate = c.RA_PreDate,
                                                             RA_ReviewDate = c.RA_ReviewDate
                                                         }).ToList(),
                                              
                                     }
                      ).FirstOrDefaultAsync();
            return Ok(getPersonalDetail);
        }
        #endregion
    }
}