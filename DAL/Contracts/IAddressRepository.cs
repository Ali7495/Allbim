using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByCode(Guid code,  CancellationToken cancellationToken);
        Task<Address> GetAddressByCodeNoTracking(Guid code,  CancellationToken cancellationToken);
        
    
    }
}