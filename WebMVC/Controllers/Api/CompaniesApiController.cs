using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompaniesApiController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesApiController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/CompaniesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // GET: api/CompaniesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // POST: api/CompaniesApi
        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(Company company)
        {
            if (await _companyService.CompanyNameExistsAsync(company.Name))
            {
                return BadRequest("Company name already exists");
            }

            var createdCompany = await _companyService.CreateCompanyAsync(company);
            return CreatedAtAction(nameof(GetCompany), new { id = createdCompany.Id }, createdCompany);
        }

        // PUT: api/CompaniesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            try
            {
                await _companyService.UpdateCompanyAsync(company);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/CompaniesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return NoContent();
        }
    }
}