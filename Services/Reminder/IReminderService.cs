
using Models.Reminder;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IReminderService
    {
        Task<List<ReminderPeriodResultViewModel>> getAllPeriod(CancellationToken cancellationToken);
        Task<List<ReminderResultViewModel>> GetAllReminder(CancellationToken cancellationToken);
        Task<ReminderResultViewModel> Create(ReminderInputViewModel viewModel, CancellationToken cancellationToken);
        Task<ReminderResultViewModel> Update(long id, ReminderInputViewModel viewModel, CancellationToken cancellationToken);
        Task<ReminderResultViewModel> UpdateMine(long UserID,long id, ReminderInputViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long id, CancellationToken cancellationToken);
        Task<bool> DeleteMine(long UserID,long id, CancellationToken cancellationToken);
        Task<ReminderResultViewModel> detail(long id, CancellationToken cancellationToken);
        Task<ReminderResultViewModel> detailMine(long UserID,long id, CancellationToken cancellationToken);
    }
}
