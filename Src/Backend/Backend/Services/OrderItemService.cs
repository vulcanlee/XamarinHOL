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

    public class OrderItemService : IOrderItemService
    {
        private readonly SchoolContext context;

        public OrderItemService(SchoolContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<OrderItem>> GetAsync()
        {
            return Task.FromResult(context.OrderItem
                .AsNoTracking()
                .Include(x => x.Product)
                .AsQueryable());
        }

        public Task<IQueryable<OrderItem>> GetByHeaderIDAsync(int id)
        {
            return Task.FromResult(context.OrderItem
                .AsNoTracking()
                .Include(x => x.Product)
                .Where(x => x.OrderId == id)
                .AsQueryable());
        }

        public async Task<OrderItem> GetAsync(int id)
        {
            OrderItem item = await context.OrderItem
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderItemId == id);
            return item;
        }

        public Task<bool> BeforeAddCheckAsync(OrderItem paraObject)
        {
            return Task.FromResult(true);
        }

        public async Task<bool> AddAsync(OrderItem paraObject)
        {
            CleanTrackingHelper.Clean<OrderItem>(context);
            await context.OrderItem.AddAsync(paraObject);
            await context.SaveChangesAsync();
            CleanTrackingHelper.Clean<OrderItem>(context);
            return true;
        }

        public Task<bool> BeforeUpdateCheckAsync(OrderItem paraObject)
        {
            return Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(OrderItem paraObject)
        {
            CleanTrackingHelper.Clean<OrderItem>(context);
            OrderItem item = await context.OrderItem
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderItemId == paraObject.OrderItemId);
            if (item == null)
            {
                return false;
            }
            else
            {
                CleanTrackingHelper.Clean<OrderItem>(context);
                context.Entry(paraObject).State = EntityState.Modified;
                await context.SaveChangesAsync();
                CleanTrackingHelper.Clean<OrderItem>(context);
                return true;
            }

        }

        public async Task<bool> DeleteAsync(OrderItem paraObject)
        {
            OrderItem item = await context.OrderItem
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderItemId == paraObject.OrderItemId);
            if (item == null)
            {
                return false;
            }
            else
            {
                CleanTrackingHelper.Clean<OrderItem>(context);
                context.Entry(item).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                CleanTrackingHelper.Clean<OrderItem>(context);
                return true;
            }
        }
    }
}
