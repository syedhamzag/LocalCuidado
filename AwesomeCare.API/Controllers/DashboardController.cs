using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Dashboard;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IGenericRepository<BaseRecordItemModel> _clientBaserecordRepository;

        public DashboardController(IGenericRepository<BaseRecordItemModel> clientBaserecordRepository)
        {
            _clientBaserecordRepository = clientBaserecordRepository;
        }
        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(type: typeof(GetDashboard), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var dashboard = new GetDashboard();
            var baseRecord = await _clientBaserecordRepository.Table.Include(s=>s.BaseRecord).ToListAsync();
            var careLabel = baseRecord.Where(s => s.BaseRecord.KeyName == "CareOjb_Status").Select(j => new { j.ValueName, j.BaseRecordItemId}).Distinct().ToList();
            var labels = baseRecord.Where(s => s.BaseRecord.KeyName == "Tele_Health_Status").Select(j => new { j.ValueName, j.BaseRecordItemId }).Distinct().ToList();
            var label = baseRecord.Where(s => s.BaseRecord.KeyName == "Staff_Communication_Status").Select(j => new { j.ValueName, j.BaseRecordItemId }).Distinct().ToList();
            var oncalllabels = baseRecord.Where(s => s.BaseRecord.KeyName == "DutyOnCall_Status").Select(j => new { j.ValueName, j.BaseRecordItemId }).Distinct().ToList();
            var concernlabels = baseRecord.Where(s => s.BaseRecord.KeyName == "TrackingConcernNote_Status").Select(j => new { j.ValueName, j.BaseRecordItemId }).Distinct().ToList();

            dashboard.nId = labels.FirstOrDefault(s => s.ValueName == "Normal").BaseRecordItemId;
            dashboard.oId = labels.FirstOrDefault(s => s.ValueName == "Under Observation").BaseRecordItemId;
            dashboard.aId = labels.FirstOrDefault(s => s.ValueName == "Urgent Attention").BaseRecordItemId;
            dashboard.rId = labels.FirstOrDefault(s => s.ValueName == "Response Received").BaseRecordItemId;

            dashboard.pId = label.FirstOrDefault(s => s.ValueName == "Pending").BaseRecordItemId;
            dashboard.cId = label.FirstOrDefault(s => s.ValueName == "Closed").BaseRecordItemId;
            dashboard.lId = label.FirstOrDefault(s => s.ValueName == "Late").BaseRecordItemId;
            dashboard.oncallP = oncalllabels.FirstOrDefault(s => s.ValueName == "Pending").BaseRecordItemId;
            dashboard.oncallC = oncalllabels.FirstOrDefault(s => s.ValueName == "Closed").BaseRecordItemId;
            dashboard.oncallO = oncalllabels.FirstOrDefault(s => s.ValueName == "Open").BaseRecordItemId;

            dashboard.ConcernIdP = concernlabels.FirstOrDefault(s => s.ValueName == "Pending").BaseRecordItemId;
            dashboard.ConcernIdC = concernlabels.FirstOrDefault(s => s.ValueName == "Closed").BaseRecordItemId;
            dashboard.ConcernIdO = concernlabels.FirstOrDefault(s => s.ValueName == "Open").BaseRecordItemId;

            dashboard.careLId = careLabel.FirstOrDefault(s => s.ValueName == "Achieved ").BaseRecordItemId;
            dashboard.carePId = careLabel.FirstOrDefault(s => s.ValueName == "Pending").BaseRecordItemId;
            dashboard.careCId = careLabel.FirstOrDefault(s => s.ValueName == "Progressing").BaseRecordItemId;
            
            
            return Ok(dashboard);
        }
    }
}
