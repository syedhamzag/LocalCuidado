using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AwesomeCare.DataTransferObject.DTOs.IncidentReporting;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientIncidentController : ControllerBase
    {
        private readonly IGenericRepository<IncidentReporting> incidentReportingRepository;
        private readonly IGenericRepository<Client> clientRepository;
        private readonly IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository;
        private readonly IGenericRepository<BaseRecordItemModel> baseRecordItemRepository;

        public ClientIncidentController(IGenericRepository<IncidentReporting> incidentReportingRepository,
            IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository)
        {
            this.incidentReportingRepository = incidentReportingRepository;
            this.clientRepository = clientRepository;
            this.staffPersonalInfoRepository = staffPersonalInfoRepository;
            this.baseRecordItemRepository = baseRecordItemRepository;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Post([FromBody] PostIncidentReport model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var incidentReporting = Mapper.Map<IncidentReporting>(model);

            await incidentReportingRepository.InsertEntity(incidentReporting);

            return Ok();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> Put([FromBody] PutIncidentReport model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var incidentReporting = Mapper.Map<IncidentReporting>(model);

            await incidentReportingRepository.UpdateEntity(incidentReporting);

            return Ok();
        }
        [HttpGet]
        [Route("Get/{id}")]
        [ProducesResponseType(typeof(GetIncidentReport), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var incidentReport = await (from incident in incidentReportingRepository.Table
                                        join client in clientRepository.Table on incident.ClientId equals client.ClientId
                                        join baseRecord in baseRecordItemRepository.Table on incident.IncidentTypeId equals baseRecord.BaseRecordItemId
                                        join staffInv in staffPersonalInfoRepository.Table on incident.StaffInvolvedId equals staffInv.StaffPersonalInfoId
                                        join st in staffPersonalInfoRepository.Table on incident.ReportingStaffId equals st.StaffPersonalInfoId
                                        where incident.IncidentReportingId == id
                                        select new GetIncidentReport
                                        {
                                            ActionTaken = incident.ActionTaken,
                                            Attachment = incident.Attachment,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = incident.ClientId,
                                            IncidentDetails = incident.IncidentDetails,
                                            IncidentTypeId = incident.IncidentTypeId,
                                            IncidentType = baseRecord.ValueName,
                                            ReportingStaff = st == null ? "" : st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                            ReportingStaffId = incident.ReportingStaffId,
                                            IncidentReportingId = incident.IncidentReportingId,
                                            StaffInvolved = staffInv.FirstName + " " + staffInv.MiddleName + " " + staffInv.LastName,
                                            StaffInvolvedId = incident.StaffInvolvedId,
                                            Witness = incident.Witness
                                        }).FirstOrDefaultAsync();

            return Ok(incidentReport);
        }
        /// <summary>
        /// Get All Incident
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(type: typeof(List<GetIncidentReport>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            var getEntities = incidentReportingRepository.Table.ToList();
            return Ok(getEntities);
        }
    }
}
