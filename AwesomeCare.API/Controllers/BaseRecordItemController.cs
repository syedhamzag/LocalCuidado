using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.BaseRecord;
using AwesomeCare.DataTransferObject.DTOs.BaseRecordItem;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseRecordItemController : ControllerBase
    {

        private IGenericRepository<BaseRecordItemModel> _baseRecordItemRepository;
        private ILogger<BaseRecordItemController> _logger;

        public BaseRecordItemController(IGenericRepository<BaseRecordItemModel> baseRecordItemRepository, ILogger<BaseRecordItemController> logger)
        {
            _baseRecordItemRepository = baseRecordItemRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetBaseRecordItem), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBaseRecordItem([FromBody]PutBaseRecordItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var baseRecordItem = Mapper.Map<BaseRecordItemModel>(item);
            var update = await _baseRecordItemRepository.UpdateEntity(baseRecordItem);

            GetBaseRecordItem getBaseRecordItems = Mapper.Map<GetBaseRecordItem>(update);
            return Ok(getBaseRecordItems);
        }

        /// <summary>
        /// Add BaseRecord Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(type: typeof(GetBaseRecordItem), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddBaseRecordItem([FromBody]PostBaseRecordItem item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var baseRecordItem = Mapper.Map<BaseRecordItemModel>(item);
            //check if Item does not exist

            var itemExist = _baseRecordItemRepository.Table.Any(i =>i.BaseRecordId==item.BaseRecordId && i.ValueName.Trim().Equals(item.ValueName.Trim(), StringComparison.InvariantCultureIgnoreCase));
            if(itemExist)
            {
                ModelState.AddModelError($"ValueName", $"Item name {item.ValueName} already exist for the selected BaseRecord");
                return BadRequest(ModelState);
            }
            var newItem = await _baseRecordItemRepository.InsertEntity(baseRecordItem);

            GetBaseRecordItem getBaseRecordItems = Mapper.Map<GetBaseRecordItem>(newItem);
            return Ok(getBaseRecordItems);
        }
    }
}