using WebMVC.Models;

namespace WebMVC.Data.Repositories
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<bool> CompanyNameExistsAsync(string name);
        Task<Company?> GetCompanyWithLLMsAsync(int id);
        Task<Company?> GetCompanyWithChatbotsAsync(int id);
    }
}