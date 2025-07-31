using WebMVC.Models;

namespace WebMVC.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company> CreateCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company company);
        Task DeleteCompanyAsync(int id);
        Task<bool> CompanyNameExistsAsync(string name);
        Task<Company?> GetCompanyWithLLMsAsync(int id);
        Task<Company?> GetCompanyWithChatbotsAsync(int id);
    }
}