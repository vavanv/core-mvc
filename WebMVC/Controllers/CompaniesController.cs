using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return View(companies);
        }

        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Company company)
        {
            if (ModelState.IsValid)
            {
                // Check if company name already exists
                if (await _companyService.CompanyNameExistsAsync(company.Name))
                {
                    ModelState.AddModelError("Name", "Company name already exists");
                    return View(company);
                }

                await _companyService.CreateCompanyAsync(company);
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _companyService.UpdateCompanyAsync(company);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Companies/GetLLMs/5 (AJAX endpoint)
        public async Task<IActionResult> GetLLMs(int id)
        {
            var company = await _companyService.GetCompanyWithLLMsAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return PartialView("_LLMsPartial", company);
        }

        // GET: Companies/GetChatbots/5 (AJAX endpoint)
        public async Task<IActionResult> GetChatbots(int id)
        {
            var company = await _companyService.GetCompanyWithChatbotsAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return PartialView("_ChatbotsPartial", company);
        }
    }
}