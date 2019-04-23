using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/Company/{companyId}/[controller]")]
    [ApiController]
    public class CompanyContactController : ControllerBase
    {

        private IGenericRepository<CompanyContactModel> _companyContactRepository;

        public CompanyContactController(IGenericRepository<CompanyContactModel> companyContactRepository)
        {
            _companyContactRepository = companyContactRepository;
        }

        [HttpGet("{id}", Name = "GetCompanyContact")]
        public async Task<IActionResult> GetCompanyContact(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var companyContact = await _companyContactRepository.GetEntity(id);

            if (companyContact == null)
            {
                return NotFound();
            }

            var companyContactDto = Mapper.Map<GetCompanyContactDto>(companyContact);

            return Ok(companyContactDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyContacts(int companyId)
        {

            var companyContacts = await _companyContactRepository.Table
               .Where(cc => cc.CompanyId == companyId)
               .ProjectTo<GetCompanyContactDto>().ToListAsync();

            return Ok(companyContacts);
        }

        [HttpPost]
        public async Task<IActionResult> PostCompanyContact(int companyId, [FromBody]PostCompanyContactDto contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var companyContactMap = Mapper.Map<CompanyContactModel>(contactDto);
            companyContactMap.CompanyId = companyId;
            var companyContact = await _companyContactRepository.InsertEntity(companyContactMap);

            var companyContactDto = Mapper.Map<GetCompanyContactDto>(companyContact);

            return CreatedAtRoute("GetCompanyContact", new { id = companyContact.CompanyContactId }, companyContactDto);
        }
    }
}