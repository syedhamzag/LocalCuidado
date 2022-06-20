using AutoMapper;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Client.CareObj;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientCareObjController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private AwesomeCareDbContext _dbContext;
        private IGenericRepository<ClientCareObj> _clientCareObjRepository;
        private IGenericRepository<StaffPersonalInfo> _staffRepository;
        private IGenericRepository<CareObjPersonToAct> _PersonToActRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;

        public ClientCareObjController(AwesomeCareDbContext dbContext, IGenericRepository<ClientCareObj> clientCareObjRepository, IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffPersonalInfo> staffRepository, IGenericRepository<BaseRecordItemModel> baseRecordItemRepository,
        IGenericRepository<CareObjPersonToAct> PersonToActRepository)
        {
            _clientCareObjRepository = clientCareObjRepository;
            _clientRepository = clientRepository;
            _dbContext = dbContext;
            _PersonToActRepository = PersonToActRepository;
            _staffRepository = staffRepository;
            _baseRecordItemRepository = baseRecordItemRepository;
        }
        #region ClientCareObj
        /// <summary>
        /// Get All ClientCareObj
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientCareObj>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = _clientCareObjRepository.Table.ToList();
            return Ok(getEntities);
        }

        /// <summary>
        /// Create ClientCareObj
        /// </summary>
        /// <param name="postClientCareObj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create([FromBody] PostClientCareObj postClientCareObj)
        {
            if (postClientCareObj == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var ClientCareObj = Mapper.Map<ClientCareObj>(postClientCareObj);
            await _clientCareObjRepository.InsertEntity(ClientCareObj);
            return Ok();
        }
        /// <summary>
        /// Update ClientCareObj
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutClientCareObj models)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var model in models.PersonToAct.ToList())
            {
                var entity = _dbContext.Set<CareObjPersonToAct>();
                var filterentity = entity.Where(c => c.CareObjId == model.CareObjId).ToList();
                if (filterentity != null)
                {
                    foreach (var item in filterentity)
                    {
                        _dbContext.Entry(item).State = EntityState.Deleted;
                    }

                }
            }
            _dbContext.SaveChanges();
            var ClientCareObj = Mapper.Map<ClientCareObj>(models);
            await _clientCareObjRepository.UpdateEntity(ClientCareObj);
            return Ok();

        }
        /// <summary>
        /// Get ClientCareObj by CareObjId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        [ProducesResponseType(type: typeof(GetClientCareObj), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientCareObj = await (from c in _clientCareObjRepository.Table
                                          join baseRecItem in _baseRecordItemRepository.Table on c.Status equals baseRecItem.BaseRecordItemId
                                          where c.CareObjId == id.Value
                                           select new GetClientCareObj
                                           {
                                               CareObjId = c.CareObjId,
                                               ClientId = c.ClientId,
                                               Date = c.Date,
                                               Remark = c.Remark,
                                               Status = c.Status,
                                               Note = c.Note,
                                               StatusName = baseRecItem.ValueName,
                                               PersonToAct = (from com in _PersonToActRepository.Table
                                                               join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                               where com.CareObjId == c.CareObjId
                                                               select new GetClientCareObjPersonToAct
                                                               {
                                                                   StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                   StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                               }).ToList()
                                           }
                      ).FirstOrDefaultAsync();
            return Ok(getClientCareObj);
        }
        /// <summary>
        /// Get ClientCareObj by CareObjId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetByClient/{id}")]
        [ProducesResponseType(type: typeof(List<GetClientCareObj>), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByClient(int? id)
        {
            if (!id.HasValue)
                return BadRequest("id Parameter is required");

            var getClientCareObj = (from c in _clientCareObjRepository.Table
                                          join baseRecItem in _baseRecordItemRepository.Table on c.Status equals baseRecItem.BaseRecordItemId
                                          where c.ClientId == id.Value
                                          select new GetClientCareObj
                                          {
                                              CareObjId = c.CareObjId,
                                              ClientId = c.ClientId,
                                              Date = c.Date,
                                              Remark = c.Remark,
                                              Status = c.Status,
                                              Note = c.Note,
                                              StatusName = baseRecItem.ValueName,
                                              PersonToAct = (from com in _PersonToActRepository.Table
                                                             join staff in _staffRepository.Table on com.StaffPersonalInfoId equals staff.StaffPersonalInfoId
                                                             where com.CareObjId == c.CareObjId
                                                             select new GetClientCareObjPersonToAct
                                                             {
                                                                 StaffPersonalInfoId = com.StaffPersonalInfoId,
                                                                 StaffName = string.Concat(staff.FirstName, " ", staff.MiddleName, " ", staff.LastName)

                                                             }).ToList()
                                          }
                      ).ToList();
            return Ok(getClientCareObj);
        }
        #endregion
    }
}
