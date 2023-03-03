using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        public ReminderRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<Reminder>> GetAllReminderPeriod(CancellationToken cancellationToken)
        {
            return await Table.Include(x => x.City).Include(x=>x.ReminderPeriod).Include(x=>x.Insurance).ToListAsync();

        }
        public async Task<Reminder> GetReminderByID(long id,CancellationToken cancellationToken)
        {
            return await Table.Include(x=>x.City).Include(x=>x.ReminderPeriod).Include(x=>x.Insurance).Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
        }
        public new async Task<List<Reminder>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await Table.Include(x=>x.City).Include(x=>x.ReminderPeriod).Include(x=>x.Insurance).ToListAsync(cancellationToken);
        }
    }
}
