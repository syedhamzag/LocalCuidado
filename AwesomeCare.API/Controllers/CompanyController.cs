using AutoMapper;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.Company;
using AwesomeCare.Model.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.API.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private IGenericRepository<CompanyModel> _companyRepository;

        public CompanyController(IGenericRepository<CompanyModel> companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet("{id}", Name = "GetCompany")]
        [EnableQuery]
        public async Task<IActionResult> GetCompany(int id)
        {            
            var company = await _companyRepository.GetEntity(id);
            var createCompanyDto = Mapper.Map<GetCompanyDto>(company);
            return Ok(createCompanyDto);
        }
        [HttpGet]
        [EnableQuery]
        public IQueryable<GetCompanyDto> GetCompanies()
        {
            var companies = _companyRepository.Table.ProjectTo<GetCompanyDto>();//.ToList();
            return companies;
           // return Ok(companies);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateCompany([FromBody]CreateCompanyDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyMap = Mapper.Map<CompanyModel>(model);
            var company = await _companyRepository.InsertEntity(companyMap);
            var createCompanyDto = Mapper.Map<CreateCompanyDto>(company);
            return CreatedAtAction("GetCompany", new { id = company.CompanyId }, createCompanyDto);
        }

        
    }
}