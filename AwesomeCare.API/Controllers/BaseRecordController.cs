using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace AwesomeCare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseRecordController : ControllerBase
    {
        private IGenericRepository<BaseRecordModel> _baseRecordRepository;
        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private ILogger<BaseRecordController> _logger;

        public BaseRecordController(IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, IGenericRepository<BaseRecordModel> baseRecordRepository, ILogger<BaseRecordController> logger)
        {
            _baseRecordItemRepository = baseRecordItemRepository;
            _baseRecordRepository = baseRecordRepository;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(type: typeof(PostBaseRecord), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostBaseRecordWithItems([FromBody]PostBaseRecord model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                    return BadRequest(ModelState);

                BaseRecordModel baseRecordMap = Mapper.Map<BaseRecordModel>(model);
                BaseRecordModel baseRecord = await _baseRecordRepository.InsertEntity(baseRecordMap);
                PostBaseRecord postBaseRecord = Mapper.Map<PostBaseRecord>(baseRecord);
                return CreatedAtAction("GetBaseRecordwithItems", new { id = baseRecord.BaseRecordId }, postBaseRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PostBaseRecord", null);
                return BadRequest();
            }
        }

        [HttpGet("GetBaseRecordWithItems/{id}", Name = "GetBaseRecordWithItems")]
        [ProducesResponseType(type: typeof(GetBaseRecordWithItems), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBaseRecordwithItems(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    ModelState.AddModelError("id", "Id Parameter must have a value");
                    return BadRequest(ModelState);
                }

                var baseRecord = await _baseRecordRepository.GetEntityWithRelatedEntity(r => r.BaseRecordItems, c => c.BaseRecordId == id);
                if (baseRecord == null)
                    return NotFound();

                GetBaseRecordWithItems getBaseRecord = Mapper.Map<GetBaseRecordWithItems>(baseRecord);
                return Ok(getBaseRecord);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "GetBaseRecordwithItems", null);
                return BadRequest();
            }
        }

        [HttpGet("{id}", Name = "GetBaseRecord")]
        [ProducesResponseType(type: typeof(GetBaseRecord), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBaseRecordByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                ModelState.AddModelError("id", "Id Parameter must have a value");
                return BadRequest(ModelState);
            }

            var baseRecord = await _baseRecordRepository.GetEntity(id);
            if (baseRecord == null)
                return NotFound();

            GetBaseRecord getBaseRecord = Mapper.Map<GetBaseRecord>(baseRecord);
            return Ok(getBaseRecord);
        }


        [HttpGet(Name = "GetBaseRecords")]
        [ProducesResponseType(type: typeof(List<GetBaseRecord>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBaseRecordsAsync()
        {           
            var baseRecords = await _baseRecordRepository.Table.ProjectTo<GetBaseRecord>().ToListAsync();           
            return Ok(baseRecords);
        }

        [HttpGet("GetBaseRecordsWithItems", Name = "GetBaseRecordsWithItems")]
        [ProducesResponseType(type: typeof(List<GetBaseRecordWithItems>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBaseRecordsWithItemsAsync()
        {
            var baseRecords = await _baseRecordRepository.Table.ProjectTo<GetBaseRecordWithItems>().Include(c=>c.BaseRecordItems).ToListAsync();
            return Ok(baseRecords);
        }


        [HttpGet("GetBaseRecordItemById/{baseRecordItemId}", Name = "GetBaseRecordItemById")]
        [ProducesResponseType(type: typeof(GetBaseRecordItem), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBaseRecordItemById(int? baseRecordItemId)
        {
            if (!baseRecordItemId.HasValue)
            {
                ModelState.AddModelError("baseRecordItemId", "baseRecordItemId Parameter must have a value");
                return BadRequest(ModelState);
            }

            var baseRecordItem = await _baseRecordItemRepository.GetEntity(baseRecordItemId);
            if (baseRecordItem == null)
                return NotFound();

            GetBaseRecordItem getBaseRecordItems = Mapper.Map<GetBaseRecordItem>(baseRecordItem);
            return Ok(getBaseRecordItems);
        }
    }
}