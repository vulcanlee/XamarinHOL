using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IHoluserService
    {
        Task<bool> AddAsync(Holuser paraObject);
        Task<bool> DeleteAsync(Holuser paraObject);
        Task<IQueryable<Holuser>> GetAsync();
        Task<Holuser> GetAsync(int id);
        Task<bool> UpdateAsync(Holuser paraObject);
        Task<(Holuser,string)> CheckUser(string account, string password);
    }
}