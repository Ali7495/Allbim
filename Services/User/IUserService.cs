using System;
using Common.Utilities;
using DAL.Models;
using Models.User;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.PageAble;

namespace Services
{
    public interface IUserService
    {
        Task<UserResultViewModel> Create(UserInputViewModel userInputViewModel, CancellationToken cancellationToken);
        Task<UserResultViewModel> Update(Guid userCode, UserUpdateInputViewModel userViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(Guid code, CancellationToken cancellationToken);
        //Task<User> detail(string code, CancellationToken cancellationToken);
        Task<PagedResult<User>> all(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<List<UserInfoViewModel>> GetData(string parameter, CancellationToken cancellationToken);
        Task<UserInfoViewModel> AssignUserRole(Guid code, long role_id, CancellationToken cancellationToken);
        Task<UserInfoViewModel> DeleteUserRole(Guid code, long role_id, CancellationToken cancellationToken);



        Task<UserInfoViewModel> GetDataMine(long userId, CancellationToken cancellationToken);
        Task<UserInfoViewModel> UpdateMine(long userId, UserViewModel userViewModel, CancellationToken cancellationToken);
        Task<UserResultViewModel> GetUser(Guid userCode, CancellationToken cancellationToken);
        Task<PagedResult<UserResultViewModel>> GetUsers(PageAbleResult pageAbleResult, CancellationToken cancellationToken);




        Task<string> ChangeUserPasswordCommon(Guid UserCode, UserChangePasswordViewModel model, CancellationToken cancellationToken);
        Task<string> ChangeUserPassword(Guid UserCode, UserChangePasswordViewModel model, CancellationToken cancellationToken);
        Task<string> ChangeUserPasswordMine(long userId, MineUserChangePasswordViewModel model, CancellationToken cancellationToken);
    }
}
