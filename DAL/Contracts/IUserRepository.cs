using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using Models.User;

namespace DAL.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task<User> GetByUserAndPassWithRoles(string username, string password, CancellationToken cancellationToken);
        Task<List<Role>> GetUserRolesAsync(long userId);
        Task<List<Permission>> GetRolesPermissionsAsync(IEnumerable<string> roles);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task<User> GetUserByCode(Guid code, CancellationToken cancellationToken);
        Task<User> GetUserByCodeNoTracking(Guid code, CancellationToken cancellationToken);
        Task<User> GetByUserName(string userName);
        Task<User> GetByUserNameCode(CheckVerificationViewModel model);
        Task<User> GetByUserNameVerificationCode(string verificationCode1, string verificationCode2);
        Task<User> GetByUserNameChangePassword(ChangePasswordViewModel model, string username);
        Task<User> GetwithPersonById(CancellationToken cancellationToken, long id);
        Task<User> GetwithPersonByIdTracked(CancellationToken cancellationToken, long id);
        Task<List<User>> GetData(DateTime? parameter, CancellationToken cancellationToken);
        Task<User> GetWithPerson(long id);
        Task<User> GetUserByIdNoTracking(long userId, CancellationToken cancellationToken);
        Task<User> GetUserByPersonIdNoTracking(long personId, CancellationToken cancellationToken);
        Task<User> GetUserByCodeWithDetail(Guid userCode, CancellationToken cancellationToken);
        Task<PagedResult<User>> GetAllUsersByCodeWithDetail(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<User> GetUserByPersonCode(Guid personCode, CancellationToken cancellationToken);
        Task<User> GetByUserNameNoTracking(string userName);
    }
}