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
    public interface ICityRepository: IRepository<City>
    {

        Task<List<City>> GetAllWithProvince( CancellationToken cancellationToken);
    }
}
