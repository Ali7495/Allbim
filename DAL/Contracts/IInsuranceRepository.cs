using DAL.Models;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IInsuranceRepository : IRepository<Insurance>
    {
        Task<Insurance> GetInsurance(string slug, CancellationToken cancellationToken);
        Task<Insurance> GetInsurancesWithDetailBySlug(string slug, CancellationToken cancellationToken);
        Task<Insurance> GetInsuranceById(long id, CancellationToken cancellationToken);
        Task<Insurance> GetBySlugAndInsurer(string slug, long insurerId, CancellationToken cancellationToken);
        Task<List<Insurance>> GetInsuranceDetails(string slug, CancellationToken cancellationToken);
        // Task<ICollection<Insurer>> GetInsurersBySlug(string slug, CancellationToken cancellationToken);

        Task<Insurance> GetInsurerWithRelation(long id, CancellationToken cancellationToken);

        Task<Insurance> GetInsurancesWithStepsBySlug(string slug, CancellationToken cancellationToken);

        Task<Insurance> GetInsuranceByIdNoTracking(long id, CancellationToken cancellationToken);

    }
}
