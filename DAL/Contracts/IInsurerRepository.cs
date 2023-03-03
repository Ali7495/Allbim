using DAL.Models;
using Models.Center;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IInsurerRepository: IRepository<Insurer>
    {

        Task<List<Insurer>> GetByInsuranceSlug(string slug, CancellationToken cancellationToken);
        Task<Insurer> GetInsurerWithInsuranceById(long id, CancellationToken cancellationToken);
        Task<Insurer> GetWithInsuranceIdAndCompanyCode(Guid code, long id, CancellationToken cancellationToken);
        Task<Insurer> GetWithInsuranceIdAndCompanyCodeNoTracking(Guid code, long id, CancellationToken cancellationToken);
        Task<List<Insurer>> GetAllInsurerWithCompanyCode(Guid code, CancellationToken cancellationToken);
        Task<Insurer> GetInsurerById(long id, CancellationToken cancellationToken);
        Task<List<Insurer>> GetAllInsurersByInsuranceId(long id, CancellationToken cancellationToken);

        Task<List<Insurer>> GetAllInsurersWithTermsByInsuranceId(long insuranceId, CancellationToken cancellationToken);
    }
}
