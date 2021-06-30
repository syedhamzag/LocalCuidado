using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRotaName;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaNameController : ControllerBase
    {
        private IGenericRepository<Rota> _rotaRepository;
        private readonly IGenericRepository<StaffRota> staffRotaRepository;

        public ClientRotaNameController(IGenericRepository<Rota> rotaRepository,
             IGenericRepository<StaffRota> staffRotaRepository)
        {
            _rotaRepository = rotaRepository;
            this.staffRotaRepository = staffRotaRepository;
        }

        /// <summary>
        /// Get Rota by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaNameById")]
        [ProducesResponseType(type: typeof(GetClientRotaName), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _rotaRepository.Table.ProjectTo<GetClientRotaName>().FirstOrDefault(d => d.RotaId == id && !d.Deleted);

            return Ok(getEntity);
        }
        /// <summary>
        /// Create Rota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(type: typeof(GetClientRotaName), statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PostClientRotaName model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool isRotaNameRegistered = _rotaRepository.Table.Any(r => r.RotaName.ToLower() == model.RotaName.ToLower());
            if (isRotaNameRegistered)
            {
                return BadRequest($"Rota name {model.RotaName} already exist");
            }
            var postRota = Mapper.Map<Rota>(model);
            var newRota = await _rotaRepository.InsertEntity(postRota);
            var getEntity = Mapper.Map<GetClientRotaName>(newRota);

            return CreatedAtRoute("GetClientRotaNameById", new { id = getEntity.RotaId }, getEntity);
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetClientRotaName>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {

            var getEntities = _rotaRepository.Table.Where(r => !r.Deleted).ProjectTo<GetClientRotaName>().ToList();
            return Ok(getEntities);
        }

        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRotaName), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PutClientRotaName model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _rotaRepository.GetEntity(model.RotaId);
            var putRota = Mapper.Map(model, entity);
            var updateEntity = await _rotaRepository.UpdateEntity(putRota);
            var getEntity = Mapper.Map<GetClientRotaName>(updateEntity);

            return Ok(getEntity);


        }

        /// <summary>
        /// comma separated dates to exclude, Format yyyy-MM-dd
        /// </summary>
        /// <param name="excludeDates"></param>
        /// <returns></returns>
        [HttpGet("ExcludeRotaByDates/{excludeDates}")]
        [ProducesResponseType(type: typeof(List<GetClientRotaName>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string excludeDates)
        {
            string format = "yyyy-MM-dd";
            List<DateTime> dateTimes = new List<DateTime>();
            if (excludeDates.Contains(","))
            {
                var dates = excludeDates.Split(",");
                foreach (var date in dates)
                {
                    if (DateTime.TryParseExact(date, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime singleDate))
                    {
                        if (!dateTimes.Contains(singleDate))
                            dateTimes.Add(singleDate);
                    }
                }
            }
            else
            {
                if (DateTime.TryParseExact(excludeDates, format, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out DateTime singleDate))
                {
                    if (!dateTimes.Contains(singleDate))
                        dateTimes.Add(singleDate);
                }
            }

            var rotas = staffRotaRepository.Table.Where(r => dateTimes.Contains(r.RotaDate)).Select(id => id.RotaId).ToList();


            var getEntities = _rotaRepository.Table.Where(r => !r.Deleted && !rotas.Contains(r.RotaId)).ProjectTo<GetClientRotaName>().ToList();
            return Ok(getEntities);
        }
    }
}