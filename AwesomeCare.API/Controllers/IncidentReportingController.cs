using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IncidentReportingController : ControllerBase
    {
        private readonly IGenericRepository<StaffIncidentReporting> staffIncidentReportingRepository;
        private readonly IGenericRepository<Client> clientRepository;
        private readonly IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository;
        private readonly IGenericRepository<BaseRecordItemModel> baseRecordItemRepository;
        private readonly IGenericRepository<ApplicationUser> applicationUserRepository;

        public IncidentReportingController(IGenericRepository<StaffIncidentReporting> staffIncidentReportingRepository,
            IGenericRepository<Client> clientRepository,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
            IGenericRepository<BaseRecordItemModel> baseRecordItemRepository,
            IGenericRepository<ApplicationUser> applicationUserRepository)
        {
            this.staffIncidentReportingRepository = staffIncidentReportingRepository;
            this.clientRepository = clientRepository;
            this.staffPersonalInfoRepository = staffPersonalInfoRepository;
            this.baseRecordItemRepository = baseRecordItemRepository;
            this.applicationUserRepository = applicationUserRepository;
        }
        [HttpPost]
        [Route("Staff/IncidentReport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StaffIncidentReport([FromBody] PostReportStaff model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var incidentReporting = Mapper.Map<StaffIncidentReporting>(model);
            incidentReporting.LoggedById = this.User.SubClaim();

            await staffIncidentReportingRepository.InsertEntity(incidentReporting);

            return Ok();
        }

        [HttpGet]
        [Route("Staff/IncidentReport/{id}")]
        [ProducesResponseType(typeof(GetStaffIncidentReport), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReportStaffById(int id)
        {
            var incidentReport = await (from incident in staffIncidentReportingRepository.Table
                                        join client in clientRepository.Table on incident.ClientId equals client.ClientId
                                        join baseRecord in baseRecordItemRepository.Table on incident.IncidentType equals baseRecord.BaseRecordItemId
                                        join staffInv in staffPersonalInfoRepository.Table on incident.StaffInvolvedId equals staffInv.StaffPersonalInfoId
                                        join user in applicationUserRepository.Table on incident.LoggedById equals user.Id
                                        join reportingStaff in staffPersonalInfoRepository.Table on incident.ReportingStaffId equals reportingStaff.StaffPersonalInfoId into rpS
                                        from st in rpS.DefaultIfEmpty()
                                        where incident.StaffIncidentReportingId == id
                                        select new GetStaffIncidentReport
                                        {
                                            ActionTaken = incident.ActionTaken,
                                            Attachment = incident.Attachment,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = incident.ClientId,
                                            IncidentDetails = incident.IncidentDetails,
                                            IncidentTypeId = incident.IncidentType,
                                            IncidentType = baseRecord.ValueName,
                                            LoggedBy = "",
                                            LoggedById = incident.LoggedById,
                                            LoggedDate = incident.LoggedDate,
                                            ReportingStaff = st == null ? "" : st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                            ReportingStaffId = incident.ReportingStaffId,
                                            StaffIncidentReportingId = incident.StaffIncidentReportingId,
                                            StaffInvolved = staffInv.FirstName + " " + staffInv.MiddleName + " " + staffInv.LastName,
                                            StaffInvolvedId = incident.StaffInvolvedId,
                                            Witness = incident.Witness
                                        }).FirstOrDefaultAsync();

            return Ok(incidentReport);
        }

        [HttpGet]
        [Route("Staff/IncidentReport")]
        [ProducesResponseType(typeof(List<GetStaffIncidentReport>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIncidentReports()
        {
            var incidentReport = await (from incident in staffIncidentReportingRepository.Table
                                        join client in clientRepository.Table on incident.ClientId equals client.ClientId
                                        join baseRecord in baseRecordItemRepository.Table on incident.IncidentType equals baseRecord.BaseRecordItemId
                                        join staffInv in staffPersonalInfoRepository.Table on incident.StaffInvolvedId equals staffInv.StaffPersonalInfoId
                                        join user in applicationUserRepository.Table on incident.LoggedById equals user.Id
                                        join reportingStaff in staffPersonalInfoRepository.Table on incident.ReportingStaffId equals reportingStaff.StaffPersonalInfoId into rpS
                                        from st in rpS.DefaultIfEmpty()
                                        select new GetStaffIncidentReport
                                        {
                                            ActionTaken = incident.ActionTaken,
                                            Attachment = incident.Attachment,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = incident.ClientId,
                                            IncidentDetails = incident.IncidentDetails,
                                            IncidentTypeId = incident.IncidentType,
                                            IncidentType = baseRecord.ValueName,
                                            LoggedBy = "",
                                            LoggedById = incident.LoggedById,
                                            LoggedDate = incident.LoggedDate,
                                            ReportingStaff = st == null ? "" : st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                            ReportingStaffId = incident.ReportingStaffId,
                                            StaffIncidentReportingId = incident.StaffIncidentReportingId,
                                            StaffInvolved = staffInv.FirstName + " " + staffInv.MiddleName + " " + staffInv.LastName,
                                            StaffInvolvedId = incident.StaffInvolvedId,
                                            Witness = incident.Witness
                                        }).ToListAsync();

            return Ok(incidentReport);
        }


        [HttpGet]
        [Route("Staff/IncidentReport/BySignedInUser")]
        [ProducesResponseType(typeof(List<GetStaffIncidentReport>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIncidentReportsByLoggedInUser()
        {
            var staffPersonalInfoId = int.TryParse(this.User.StaffPersonalInfoId(), out int id) ? id : 0;

            var incidentReport = await (from incident in staffIncidentReportingRepository.Table
                                        join client in clientRepository.Table on incident.ClientId equals client.ClientId
                                        join baseRecord in baseRecordItemRepository.Table on incident.IncidentType equals baseRecord.BaseRecordItemId
                                        join staffInv in staffPersonalInfoRepository.Table on incident.StaffInvolvedId equals staffInv.StaffPersonalInfoId
                                        join user in applicationUserRepository.Table on incident.LoggedById equals user.Id
                                        join reportingStaff in staffPersonalInfoRepository.Table on incident.ReportingStaffId equals reportingStaff.StaffPersonalInfoId into rpS
                                        from st in rpS.DefaultIfEmpty()
                                        where incident.ReportingStaffId == staffPersonalInfoId || incident.StaffInvolvedId == staffPersonalInfoId
                                        select new GetStaffIncidentReport
                                        {
                                            ActionTaken = incident.ActionTaken,
                                            Attachment = incident.Attachment,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = incident.ClientId,
                                            IncidentDetails = incident.IncidentDetails,
                                            IncidentTypeId = incident.IncidentType,
                                            IncidentType = baseRecord.ValueName,
                                            LoggedBy = "",
                                            LoggedById = incident.LoggedById,
                                            LoggedDate = incident.LoggedDate,
                                            ReportingStaff = st == null ? "" : st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                            ReportingStaffId = incident.ReportingStaffId,
                                            StaffIncidentReportingId = incident.StaffIncidentReportingId,
                                            StaffInvolved = staffInv.FirstName + " " + staffInv.MiddleName + " " + staffInv.LastName,
                                            StaffInvolvedId = incident.StaffInvolvedId,
                                            Witness = incident.Witness
                                        }).ToListAsync();

            return Ok(incidentReport);
        }

        [HttpGet]
        [Route("Staff/IncidentReport/BySignedInUser/{id}")]
        [ProducesResponseType(typeof(GetStaffIncidentReport), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIncidentReportsByLoggedInUser(int id)
        {
            var staffPersonalInfoId = int.TryParse(this.User.StaffPersonalInfoId(), out int staffid) ? staffid : 0;

            var incidentReport = await (from incident in staffIncidentReportingRepository.Table
                                        join client in clientRepository.Table on incident.ClientId equals client.ClientId
                                        join baseRecord in baseRecordItemRepository.Table on incident.IncidentType equals baseRecord.BaseRecordItemId
                                        join staffInv in staffPersonalInfoRepository.Table on incident.StaffInvolvedId equals staffInv.StaffPersonalInfoId
                                        join user in applicationUserRepository.Table on incident.LoggedById equals user.Id
                                        join reportingStaff in staffPersonalInfoRepository.Table on incident.ReportingStaffId equals reportingStaff.StaffPersonalInfoId into rpS
                                        from st in rpS.DefaultIfEmpty()
                                        where incident.StaffIncidentReportingId == id && incident.ReportingStaffId == staffPersonalInfoId || incident.StaffInvolvedId == staffPersonalInfoId
                                        select new GetStaffIncidentReport
                                        {
                                            ActionTaken = incident.ActionTaken,
                                            Attachment = incident.Attachment,
                                            Client = client.Firstname + " " + client.Middlename + " " + client.Surname,
                                            ClientId = incident.ClientId,
                                            IncidentDetails = incident.IncidentDetails,
                                            IncidentTypeId = incident.IncidentType,
                                            IncidentType = baseRecord.ValueName,
                                            LoggedBy = "",
                                            LoggedById = incident.LoggedById,
                                            LoggedDate = incident.LoggedDate,
                                            ReportingStaff = st == null ? "" : st.FirstName + " " + st.MiddleName + " " + st.LastName,
                                            ReportingStaffId = incident.ReportingStaffId,
                                            StaffIncidentReportingId = incident.StaffIncidentReportingId,
                                            StaffInvolved = staffInv.FirstName + " " + staffInv.MiddleName + " " + staffInv.LastName,
                                            StaffInvolvedId = incident.StaffInvolvedId,
                                            Witness = incident.Witness
                                        }).FirstOrDefaultAsync();

            return Ok(incidentReport);
        }
    }
}
