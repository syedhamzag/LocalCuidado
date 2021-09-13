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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IGenericRepository<BaseRecordItemModel> _clientBaserecordRepository;

        public DashboardController(IGenericRepository<BaseRecordItemModel> clientBaserecordRepository)
        {
            _clientBaserecordRepository = clientBaserecordRepository;
        }

        [HttpGet()]
        [ProducesResponseType(type: typeof(GetDashboard), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var dashboard = new GetDashboard();

            var labels = await _clientBaserecordRepository.Table.Where(s => s.BaseRecord.KeyName == "Tele_Health_Status").Select(j => new { j.ValueName, j.BaseRecordItemId }).Distinct().ToListAsync();
            var label = await _clientBaserecordRepository.Table.Where(s => s.BaseRecord.KeyName == "Staff_Communication_Status").Select(j => new { j.ValueName, j.BaseRecordItemId }).Distinct().ToListAsync();

            dashboard.nId = labels.FirstOrDefault(s => s.ValueName == "Normal").BaseRecordItemId;
            dashboard.oId = labels.FirstOrDefault(s => s.ValueName == "Under Observation").BaseRecordItemId;
            dashboard.aId = labels.FirstOrDefault(s => s.ValueName == "Urgent Attention").BaseRecordItemId;
            dashboard.rId = labels.FirstOrDefault(s => s.ValueName == "Response Received").BaseRecordItemId;

            dashboard.pId = label.FirstOrDefault(s => s.ValueName == "Pending").BaseRecordItemId;
            dashboard.cId = label.FirstOrDefault(s => s.ValueName == "Closed").BaseRecordItemId;
            dashboard.lId = label.FirstOrDefault(s => s.ValueName == "Late").BaseRecordItemId;

            return Ok(dashboard);
        }
    }
}
