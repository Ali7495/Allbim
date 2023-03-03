using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts.EnumIRepositories
{
    public interface IThirdInsuranceCreditMonthRepository
    {
        Task<List<Enumeration>> GetAllAsync(CancellationToken cancellationToken);
    }
}
