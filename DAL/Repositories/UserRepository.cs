using Common.Extensions;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Utilities;
using Models.PageAble;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;

        public UserRepository(AlbimDbContext dbContext, IRepository<UserRole> userRoleRepository,
            IRepository<Role> roleRepository) : base(dbContext)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return await Table.Where(p => p.Username == username && p.Password == passwordHash)
                .SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<User> GetByUserAndPassWithRoles(string username, string password, CancellationToken cancellationToken)
        {
            var passwordHash = SecurityHelper.GetSha256Hash(password);
            return await Table.Include(x=>x.UserRoles).Where(p => p.Username == username && p.Password == passwordHash)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
        {
            var exists = await Table.AsNoTracking().AnyAsync(p => p.Username == user.Username);
            if (exists)
                throw new BadRequestException("نام کاربری تکراری است");

            var passwordHash = SecurityHelper.GetSha256Hash(password);
            user.Password = passwordHash;
            await base.AddAsync(user, cancellationToken);
        }


        public async Task<User> GetUserByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<User> GetUserByCodeNoTracking(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Role>> GetUserRolesAsync(long userId)
        {
            var roles = await _userRoleRepository.Table.AsNoTracking().Include(z => z.Role).Where(z => z.UserId == userId)
                .Select(z => z.Role).ToListAsync();
            return roles;
        }

        public async Task<List<Permission>> GetRolesPermissionsAsync(IEnumerable<string> roles)
        {
            return await _roleRepository.Table.AsNoTracking()
                .Include(z => z.RolePermissions)
                .ThenInclude(z => z.Permission)
                .Where(z => roles.Contains(z.Name))
                .SelectMany(z => z.RolePermissions).Select(z => z.Permission).ToListAsync();
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await Table.Where(p => p.Username == userName).FirstOrDefaultAsync();
        }


        public async Task<User> GetByUserNameCode(CheckVerificationViewModel model)
        {
            return await Table
                .Where(p => p.Username == model.Username && p.TwoStepCode == model.VerificationCode &&
                            p.TwoStepExpiration >= DateTime.Now).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUserNameVerificationCode(string Username, string VerificationCode)
        {
            return await Table
                .Where(p => p.Username == Username && p.VerificationCode == VerificationCode &&
                            p.VerificationExpiration >= DateTime.Now).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUserNameChangePassword(ChangePasswordViewModel model, string username)
        {
            return await Table.Where(p => p.Username == username && p.ChangePasswordCode == model.ChangePasswordCode)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetwithPersonById(CancellationToken cancellationToken, long id)
        {
            return await Table.AsNoTracking()
                .Include(x => x.Person)
                .Where(x => x.Id == id)
                .FirstAsync(cancellationToken);
        }
        public async Task<User> GetwithPersonByIdTracked(CancellationToken cancellationToken, long id)
        {
            return await Table
                .Include(x => x.Person)
                .Where(x => x.Id == id)
                .FirstAsync(cancellationToken);
        }

        public async Task<List<User>> GetData(DateTime? parameter, CancellationToken cancellationToken)
        {
            return await Table.Include(x => x.Person).Where(p => p.LastLogOnDate.Value >= parameter).ToListAsync();
        }


        public async Task<User> GetWithPerson(long id)
        {
            return await Table.Include(x => x.Person).FirstAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByIdNoTracking(long userId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Id == userId).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetUserByPersonIdNoTracking(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.PersonId == personId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> GetUserByCodeWithDetail(Guid userCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.Code == userCode).Include(i => i.Person).Include(i => i.UserRoles).ThenInclude(th => th.Role).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<User>> GetAllUsersByCodeWithDetail(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Person).Include(i => i.UserRoles).ThenInclude(th => th.Role).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<User> GetUserByPersonCode(Guid personCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(u => u.Person.Code == personCode).Include(i=> i.Person).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<User> GetByUserNameNoTracking(string userName)
        {
            return await Table.AsNoTracking().Where(p => p.Username == userName).FirstOrDefaultAsync();
        }
       
    }
}