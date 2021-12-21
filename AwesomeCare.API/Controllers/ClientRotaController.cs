using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Database;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.ClientRota;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientRotaController : ControllerBase
    {
        private IGenericRepository<ClientRota> _clientRotaRepository;
        private IGenericRepository<ClientRotaDays> _clientRotaDaysRepository;
        private IGenericRepository<ClientRotaTask> _clientRotaTaskRepository;
        private AwesomeCareDbContext _dbContext;

        public ClientRotaController(IGenericRepository<ClientRota> clientRotaRepository,
            IGenericRepository<ClientRotaDays> clientRotaDaysRepository,
            IGenericRepository<ClientRotaTask> clientRotaTaskRepository,
            AwesomeCareDbContext dbContext)
        {
            _clientRotaRepository = clientRotaRepository;
            _clientRotaDaysRepository = clientRotaDaysRepository;
            _clientRotaTaskRepository = clientRotaTaskRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get ClientRota by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetClientRotaById")]
        [ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Parameter id is required");
            }

            var getEntity = _clientRotaRepository.Table.ProjectTo<GetClientRota>().FirstOrDefault(d => d.ClientRotaId == id);

            return Ok(getEntity);
        }

        /// <summary>
        /// Get ClientRota for Edit
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetForEdit/{id}")]
        [ProducesResponseType(type: typeof(List<GetClientRota>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetForEdit(int id)
        {
            var rotas = GetClientRota(id);
            return Ok(rotas);
        }

        [HttpPut("Edit/{id}")]
        public IActionResult Edit([FromBody]List<CreateClientRota> model, int id)
        {
            var clientRotaEntity = _dbContext.Set<ClientRota>();
            var rotas = clientRotaEntity.Where(c => c.ClientId == id).Include(d => d.ClientRotaDays).ThenInclude(t => t.ClientRotaTask).ToList(); //    GetClientRota(id);//from database

            

            foreach (var rota in rotas)
            {
                //check if rota in db is part of the rotas in Model if not mark as Deleted
                var currentRota = model.FirstOrDefault(r => r.ClientRotaTypeId == rota.ClientRotaTypeId);
                if (currentRota == null)
                {
                    //delete from database
                    _dbContext.Entry(rota).State = EntityState.Deleted;
                }
                else
                {
                    //Rota in Db is present in Model
                    foreach (var rotaDayDb in rota.ClientRotaDays.ToList())
                    {

                        var currentDbRotaDay = currentRota.ClientRotaDays.FirstOrDefault(d => d.ClientRotaDaysId == rotaDayDb.ClientRotaDaysId);
                        if (currentDbRotaDay != null)
                        {
                            rotaDayDb.ClientRotaDaysId = currentDbRotaDay.ClientRotaDaysId;
                            rotaDayDb.RotaDayofWeekId = currentDbRotaDay.RotaDayofWeekId;
                            rotaDayDb.StartTime = currentDbRotaDay.StartTime;
                            rotaDayDb.StopTime = currentDbRotaDay.StopTime;
                            rotaDayDb.RotaId = currentDbRotaDay.RotaId;
                            rotaDayDb.ClientRotaId = rotaDayDb.ClientRotaId;
                            rotaDayDb.ClientId = currentDbRotaDay.ClientId;
                            rotaDayDb.ClientRotaTypeId = currentDbRotaDay.ClientRotaTypeId;

                            _dbContext.Entry(rotaDayDb).State = EntityState.Modified;
                        }
                        //check records in db that are not in model, then mark as Deleted
                        foreach (var rotaTaskDb in rotaDayDb.ClientRotaTask.ToList())
                        {
                            var currentDbRotaTask = currentDbRotaDay.RotaTasks.FirstOrDefault(r => r.RotaTaskId == rotaTaskDb.RotaTaskId);
                            if (currentDbRotaTask == null)
                            {
                                _dbContext.Entry(rotaTaskDb).State = EntityState.Deleted;
                            }
                        }
                        //check records in model that are not in db, then mark as Added
                        foreach (var rotaTaskModel in currentDbRotaDay.RotaTasks)
                        {
                            var currentModelRotaTask = rotaDayDb.ClientRotaTask.FirstOrDefault(r => r.RotaTaskId == rotaTaskModel.RotaTaskId);
                            if (currentModelRotaTask == null)//not in Database
                            {
                                var newRotaTask = new ClientRotaTask();
                                newRotaTask.ClientRotaDaysId = rotaDayDb.ClientRotaDaysId;
                                newRotaTask.RotaTaskId = rotaTaskModel.RotaTaskId;

                                _dbContext.Entry(newRotaTask).State = EntityState.Added;
                            }
                        }
                    }
                }
            }

            //Rotas from Model not in Database
            foreach (var item in model)
            {
                var rotaNotInDb = rotas.FirstOrDefault(r => r.ClientRotaTypeId == item.ClientRotaTypeId);
                if(rotaNotInDb == null)
                {
                    var postEntity = Mapper.Map<ClientRota>(item);
                    _dbContext.Entry(postEntity).State = EntityState.Added;
                    foreach (var dy in postEntity.ClientRotaDays)
                    {
                        _dbContext.Entry(dy).State = EntityState.Added;
                        foreach (var tk in dy.ClientRotaTask)
                        {
                            _dbContext.Entry(tk).State = EntityState.Added;
                        }
                    }
                  
                }
            }
            var result = _dbContext.SaveChanges();
            return Ok();

        }

        List<GetClientRota> GetClientRota(int clientId)
        {
            var rotas = (from c in _clientRotaRepository.Table
                         where c.ClientId == clientId
                         select new GetClientRota
                         {
                             ClientId = c.ClientId,
                             ClientRotaId = c.ClientRotaId,
                             ClientRotaTypeId = c.ClientRotaTypeId,
                             ClientRotaDays = (from d in c.ClientRotaDays
                                               select new GetClientRotaDay
                                               {
                                                   ClientRotaDaysId = d.ClientRotaDaysId,
                                                   RotaDayofWeekId = d.RotaDayofWeekId,
                                                   StartTime = d.StartTime,
                                                   StopTime = d.StopTime,
                                                   RotaId = d.RotaId,
                                                   RotaTasks = (from t in d.ClientRotaTask
                                                                select new GetClientTask
                                                                {
                                                                    ClientRotaTaskId = t.ClientRotaTaskId,
                                                                    RotaTaskId = t.RotaTaskId,
                                                                    ClientRotaDaysId = d.ClientRotaDaysId
                                                                }).ToList()
                                               }).ToList()
                         }).ToList();

            return rotas;
        }
        ///// <summary>
        ///// Create ClientRota
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost()]
        //[Route("[action]")]
        //[ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post([FromBody]PostClientRota model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }


        //    var postEntity = Mapper.Map<ClientRota>(model);
        //    var newEntity = await _clientRotaRepository.InsertEntity(postEntity);
        //    var getEntity = Mapper.Map<GetClientRota>(newEntity);

        //    return CreatedAtRoute("GetClientRotaById", new { id = getEntity.ClientRotaId }, getEntity);
        //}

        /// <summary>
        /// Update ClientRota
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(type: typeof(GetClientRota), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody]PutClientRota model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await _clientRotaRepository.GetEntity(model.ClientRotaId);
            var putEntity = Mapper.Map(model, entity);
            var updateEntity = await _clientRotaRepository.UpdateEntity(putEntity);
            var getEntity = Mapper.Map<GetClientRota>(updateEntity);

            return Ok(getEntity);


        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateRota([FromBody]List<CreateClientRota> model)
        {
            var clientId = model.FirstOrDefault()?.ClientId;
            var hasRota = _clientRotaRepository.Table.Any(c => c.ClientId == clientId);
            if (hasRota)
            {
                ModelState.AddModelError("", "Client already as rota created");
                return BadRequest(model);
            }
            var postEntity = Mapper.Map<List<ClientRota>>(model);
            await _clientRotaRepository.InsertEntities(postEntity);
            return Ok();
        }
    
    
    [HttpGet("RotaPeriod/{clientId}/{clientRotaTypeId}/{rotaDayOfWeekId}/{rotaId}")]
    public async Task<IActionResult> GetClientRotaPeriod(int clientId,int clientRotaTypeId,int rotaDayOfWeekId,int rotaId)
        {
            var periods =await (from clientRota in _clientRotaRepository.Table
                           join clientRotaDay in _clientRotaDaysRepository.Table on clientRota.ClientRotaId equals clientRotaDay.ClientRotaId
                           where clientRota.ClientId == clientId &&
                           clientRota.ClientRotaTypeId == clientRotaTypeId &&
                           clientRotaDay.RotaDayofWeekId == rotaDayOfWeekId &&
                           clientRotaDay.RotaId == rotaId
                           select new
                           {
                               ClientRotaId = clientRota.ClientRotaId,
                               ClientId = clientRota.ClientId,
                               ClientRotaTypeId = clientRota.ClientRotaTypeId,
                               RotaDayofWeekId = clientRotaDay.RotaDayofWeekId,
                               StartTime = clientRotaDay.StartTime,
                               StopTime = clientRotaDay.StopTime,
                               RotaId = clientRotaDay.RotaId,
                               ClientRotaDaysId = clientRotaDay.ClientRotaDaysId
                           }).FirstOrDefaultAsync();

            return Ok(periods);
        }



       
        [HttpGet("RotaPeriod/{rotaDayOfWeekId}/{rotaId}")]
        public async Task<IActionResult> GetClientRotaPeriod(int rotaDayOfWeekId, int rotaId)
        {
            var periods = await (from clientRota in _clientRotaRepository.Table
                                 join clientRotaDay in _clientRotaDaysRepository.Table on clientRota.ClientRotaId equals clientRotaDay.ClientRotaId
                                 where
                                 clientRotaDay.RotaDayofWeekId == rotaDayOfWeekId &&
                                 clientRotaDay.RotaId == rotaId
                                 select new
                                 {
                                     ClientRotaId = clientRota.ClientRotaId,
                                     ClientId = clientRota.ClientId,
                                     ClientRotaTypeId = clientRota.ClientRotaTypeId,
                                     RotaDayofWeekId = clientRotaDay.RotaDayofWeekId,
                                     StartTime = clientRotaDay.StartTime,
                                     StopTime = clientRotaDay.StopTime,
                                     RotaId = clientRotaDay.RotaId,
                                     ClientRotaDaysId = clientRotaDay.ClientRotaDaysId
                                 }).FirstOrDefaultAsync();

            return Ok(periods);
        }
    }
}