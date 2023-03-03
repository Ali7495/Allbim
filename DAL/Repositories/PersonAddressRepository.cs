
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Contracts;
using DAL.Models;

namespace DAL.Repositories
{
    public class PersonAddressRepository : Repository<PersonAddress>, IPersonAddressRepository
    {
        public PersonAddressRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }       

        public async Task<PersonAddress> GetByPersonIdAddrerssId(long personId, long addressId, CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.AddressId == addressId && p.PersonId==personId).SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<PersonAddress> GetByPersonIdAddrerssIdNoTracking(long personId, long addressId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.AddressId == addressId && p.PersonId==personId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PersonAddress> GetPersonAddressByPersonId(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PersonId == personId && p.AddressTypeId == 1).Include(i=> i.Address).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<PersonAddress>> GetPersonAddresses(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.Person)
                .Include(x => x.Address)
                .ThenInclude(x=>x.City).ThenInclude(x=>x.TownShip).ThenInclude(x=>x.Province)
                .Where(x => x.PersonId == personId)
                .ToListAsync();
        }

       
    }
}
