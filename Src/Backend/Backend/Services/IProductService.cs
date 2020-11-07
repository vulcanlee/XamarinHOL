using DataAccessLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IProductService
    {
        Task<bool> AddAsync(Product paraObject);
        Task<bool> DeleteAsync(Product paraObject);
        Task<IQueryable<Product>> GetAsync();
        Task<Product> GetAsync(int id);
        Task<bool> UpdateAsync(Product paraObject);
    }
}