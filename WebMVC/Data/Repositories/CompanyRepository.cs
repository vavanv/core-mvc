using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Data.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CompanyNameExistsAsync(string name)
        {
            return await _context.Companies.AnyAsync(c => c.Name == name);
        }

        public async Task<Company?> GetCompanyWithLLMsAsync(int id)
        {
            return await _context.Companies
                .Include(c => c.LLMs)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company?> GetCompanyWithChatbotsAsync(int id)
        {
            return await _context.Companies
                .Include(c => c.Chatbots)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}