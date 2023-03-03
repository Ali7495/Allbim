using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        Task<List<Reminder>> GetAllReminderPeriod(CancellationToken cancellationToken);
        Task<Reminder> GetReminderByID(long id, CancellationToken cancellationToken);
        new Task<List<Reminder>> GetAllAsync(CancellationToken cancellationToken);
    }
}
