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
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(AlbimDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<UserRole> GetUserRole(long userId, long roleId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.RoleId == roleId && u.UserId == userId).Include(i=> i.User).Include(i=> i.Role).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<UserRole>> GetUserRolesByUserId(long userId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.UserId == userId).Include(i => i.Role).ToListAsync(cancellationToken);
        }

        public async Task<UserRole> GetUserRoleTracking(long userId, long roleId, CancellationToken cancellationToken)
        {
            return await Table.Where(u => u.RoleId == roleId && u.UserId == userId).Include(i=> i.User).Include(i=> i.Role).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UserRole> GetSingleUserRoleByUserId(long userId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.UserId == userId).Include(i => i.Role).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<UserRole> GetWithUserAndRole(long userId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.UserId == userId).Include(i=> i.User).Include(i => i.Role).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
