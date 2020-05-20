using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.StaffBlackList;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlackListController : ControllerBase
    {
        private IGenericRepository<Client> _clientRepository;
        private IGenericRepository<StaffBlackList> _staffBlackListRepository;
        private IGenericRepository<StaffPersonalInfo> _staffPersonalInfoRepository;
        public BlackListController(IGenericRepository<Client> clientRepository, IGenericRepository<StaffBlackList> staffBlackListRepository,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository)
        {
            _clientRepository = clientRepository;
            _staffBlackListRepository = staffBlackListRepository;
            _staffPersonalInfoRepository = staffPersonalInfoRepository;
        }

        [HttpPost("Staff")]
        [ProducesResponseType(typeof(GetStaffBlackList),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BlackListStaff([FromBody]PostStaffBlackList model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var entity = Mapper.Map<StaffBlackList>(model);
            var result = await _staffBlackListRepository.InsertEntity(entity);
            var getEntity = Mapper.Map<GetStaffBlackList>(result);

            return CreatedAtAction("GetBlackListStaff", new { id = getEntity.StaffBlackListId }, getEntity);
        }

        [HttpGet("Staff/{id}")]
        [ProducesResponseType(typeof(GetStaffBlackList), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlackListStaff(int id)
        {
            var getEntity = (from sbl in _staffBlackListRepository.Table
                            join cl in _clientRepository.Table on sbl.ClientId equals cl.ClientId
                            join st in _staffPersonalInfoRepository.Table on sbl.StaffPersonalInfoId equals st.StaffPersonalInfoId
                            where sbl.StaffBlackListId == id
                            select new GetStaffBlackList
                            {
                                StaffPersonalInfoId = st.StaffPersonalInfoId,
                                Client = cl.Firstname + " " + cl.Middlename + " " + cl.Surname,
                                ClientId = cl.ClientId,
                                Comment = sbl.Comment,
                                Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                StaffBlackListId = sbl.StaffBlackListId
                            }).FirstOrDefault();

            return Ok(getEntity);
        }

        [HttpGet("Staff")]
        [ProducesResponseType(typeof(List<GetStaffBlackList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBlackListStaff()
        {
            var entities = (from sbl in _staffBlackListRepository.Table
                            join cl in _clientRepository.Table on sbl.ClientId equals cl.ClientId
                            join st in _staffPersonalInfoRepository.Table on sbl.StaffPersonalInfoId equals st.StaffPersonalInfoId
                            select new GetStaffBlackList
                            {
                                StaffPersonalInfoId = st.StaffPersonalInfoId,
                                Client = cl.Firstname + " " + cl.Middlename + " " + cl.Surname,
                                ClientId = cl.ClientId,
                                Comment = sbl.Comment,
                                Staff = st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                StaffBlackListId = sbl.StaffBlackListId
                            }).ToList();

            return Ok(entities);
        }

        [HttpDelete("Staff/{id}")]
        [ProducesResponseType(typeof(List<GetStaffBlackList>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBlackListStaff(int id)
        {
            var result = await _staffBlackListRepository.GetEntity(id);
            if (result != null)
                await _staffBlackListRepository.DeleteEntity(result);
            return Ok();
        }
    }
}