using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    using Backend.Helpers;
    using DataAccessLayer.Models;
    using Microsoft.EntityFrameworkCore;
    using ShareBusiness.Helpers;

    public class OrderService : IOrderService
    {
        private readonly SchoolContext context;

        public OrderService(SchoolContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Order>> GetAsync()
        {
            return Task.FromResult(context.Order
                .AsNoTracking()
                .Include(x=>x.OrderItem)
                .ThenInclude(x=>x.Product)
                .AsQueryable());
        }

        public async Task<Order> GetAsync(int id)
        {
            Order item = await context.Order
                .AsNoTracking()
                .Include(x => x.OrderItem)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.OrderId == id);
            return item;
        }

        public async Task<bool> AddAsync(Order paraObject)
        {
            CleanTrackingHelper.Clean<Order>(context);
            await context.Order
                .AddAsync(paraObject);
            await context.SaveChangesAsync();
            CleanTrackingHelper.Clean<Order>(context);
            return true;
        }

        public async Task<bool> UpdateAsync(Order paraObject)
        {
            #region EF Core 追蹤查詢所造成的問題說明
            // 若再進行搜尋該修改紀錄的時候，使用了 追蹤查詢 (也就是，沒有使用 .AsNoTracking()方法)
            // 將會造成快取記錄在電腦端，而這裡若要進行 
            // context.Entry(paraObject).State = EntityState.Modified; 呼叫與更新的時候
            // 將會造成問題
            // System.InvalidOperationException: The instance of entity type 'Person' cannot be tracked 
            // because another instance with the same key value for {'PersonId'} is already being tracked. 
            // When attaching existing entities, ensure that only one entity instance with a given key value
            // is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' 
            // to see the conflicting key values.
            #endregion

            CleanTrackingHelper.Clean<Order>(context);
            Order item = await context.Order
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == paraObject.OrderId);
            if (item == null)
            {
                return false;
            }
            else
            {
                CleanTrackingHelper.Clean<Order>(context);
                context.Entry(paraObject).State = EntityState.Modified;
                await context.SaveChangesAsync();
                CleanTrackingHelper.Clean<Order>(context);
                return true;
            }

        }

        public async Task<bool> DeleteAsync(Order paraObject)
        {
            Order item = await context.Order
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderId == paraObject.OrderId);
            if (item == null)
            {
                return false;
            }
            else
            {
                CleanTrackingHelper.Clean<Order>(context);
                context.Entry(paraObject).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                CleanTrackingHelper.Clean<Order>(context);
                return true;
            }
        }
    }
}
