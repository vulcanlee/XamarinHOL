using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IOrderItemService
    {
        Task<bool> AddAsync(OrderItem paraObject);
        Task<bool> BeforeAddCheckAsync(OrderItem paraObject);
        Task<bool> BeforeUpdateCheckAsync(OrderItem paraObject);
        Task<bool> DeleteAsync(OrderItem paraObject);
        Task<IQueryable<OrderItem>> GetAsync();
        Task<OrderItem> GetAsync(int id);
        Task<IQueryable<OrderItem>> GetByHeaderIDAsync(int id);
        Task<bool> UpdateAsync(OrderItem paraObject);
    }
}