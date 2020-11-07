using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IOrderService
    {
        Task<bool> AddAsync(Order paraObject);
        Task<bool> DeleteAsync(Order paraObject);
        Task<IQueryable<Order>> GetAsync();
        Task<Order> GetAsync(int id);
        Task<bool> UpdateAsync(Order paraObject);
    }
}