using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    using ShareBusiness.Helpers;
    using DataAccessLayer.Models;
    using Microsoft.EntityFrameworkCore;

    public class HoluserService : IHoluserService
    {
        private readonly SchoolContext context;

        public HoluserService(SchoolContext context)
        {
            this.context = context;
        }

        public Task<IQueryable<Holuser>> GetAsync()
        {
            return Task.FromResult(context.Holuser
                .AsNoTracking()
                .AsQueryable());
        }

        public async Task<Holuser> GetAsync(int id)
        {
            Holuser item = await context.Holuser
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.HoluserId == id);
            return item;
        }

        public async Task<bool> AddAsync(Holuser paraObject)
        {
            CleanTrackingHelper.Clean<Holuser>(context);
            await context.Holuser
                .AddAsync(paraObject);
            await context.SaveChangesAsync();
            CleanTrackingHelper.Clean<Holuser>(context);
            return true;
        }

        public async Task<bool> UpdateAsync(Holuser paraObject)
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

            CleanTrackingHelper.Clean<Holuser>(context);
            Holuser item = await context.Holuser
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.HoluserId == paraObject.HoluserId);
            if (item == null)
            {
                return false;
            }
            else
            {
                CleanTrackingHelper.Clean<Holuser>(context);
                context.Entry(paraObject).State = EntityState.Modified;
                await context.SaveChangesAsync();
                CleanTrackingHelper.Clean<Holuser>(context);
                return true;
            }

        }

        public async Task<bool> DeleteAsync(Holuser paraObject)
        {
            CleanTrackingHelper.Clean<Holuser>(context);
            Holuser item = await context.Holuser
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.HoluserId == paraObject.HoluserId);
            if (item == null)
            {
                return false;
            }
            else
            {
                CleanTrackingHelper.Clean<Holuser>(context);
                context.Entry(paraObject).State = EntityState.Deleted;
                await context.SaveChangesAsync();
                CleanTrackingHelper.Clean<Holuser>(context);
                return true;
            }
        }

        public async Task<(Holuser,string)> CheckUser(string account, string password)
        {
            Holuser user = await context.Holuser.AsNoTracking().FirstOrDefaultAsync(x => x.Account == account);
            if(user==null)
            {
                return (null, "使用者帳號不存在");
            }

            if(user.Password != password)
            {
                return (null, "密碼不正確");
            }
            return(user,"");
        }
    }
}
