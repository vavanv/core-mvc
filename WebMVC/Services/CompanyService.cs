using Microsoft.Extensions.Caching.Memory;
using WebMVC.Data.Repositories;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "AllCompanies";

        public CompanyService(ICompanyRepository companyRepository, IMemoryCache cache)
        {
            _companyRepository = companyRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out IEnumerable<Company> companies))
            {
                companies = await _companyRepository.GetAllAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(CacheKey, companies, cacheEntryOptions);
            }
            return companies;
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _companyRepository.GetByIdAsync(id);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            company.CreatedAt = DateTime.UtcNow;
            var createdCompany = await _companyRepository.AddAsync(company);
            _cache.Remove(CacheKey);
            return createdCompany;
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {
            var updatedCompany = await _companyRepository.UpdateAsync(company);
            _cache.Remove(CacheKey);
            return updatedCompany;
        }

        public async Task DeleteCompanyAsync(int id)
        {
            await _companyRepository.DeleteAsync(id);
            _cache.Remove(CacheKey);
        }

        public async Task<bool> CompanyNameExistsAsync(string name)
        {
            return await _companyRepository.CompanyNameExistsAsync(name);
        }

        public async Task<Company?> GetCompanyWithLLMsAsync(int id)
        {
            return await _companyRepository.GetCompanyWithLLMsAsync(id);
        }

        public async Task<Company?> GetCompanyWithChatbotsAsync(int id)
        {
            return await _companyRepository.GetCompanyWithChatbotsAsync(id);
        }
    }
}