using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IPersonAddressRepository : IRepository<PersonAddress>
    {
        Task<PersonAddress> GetByPersonIdAddrerssId(long personId,long addressId,  CancellationToken cancellationToken);
        Task<PersonAddress> GetByPersonIdAddrerssIdNoTracking(long personId,long addressId,  CancellationToken cancellationToken);

        Task<List<PersonAddress>> GetPersonAddresses(long personId, CancellationToken cancellationToken);
        Task<PersonAddress> GetPersonAddressByPersonId(long personId, CancellationToken cancellationToken);
    }
}