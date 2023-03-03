using AutoMapper;
using Common.Exceptions;
using Common.Extensions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.PageAble;
using Models.Settings;
using Models.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPersonRepository _personRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IPersonRepository personRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IMapper mapper,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _personRepository = personRepository;
            _pagingSettings = pagingSettings.Value;
            _mapper = mapper;
        }



        public async Task<UserResultViewModel> Create(UserInputViewModel userInputViewModel, CancellationToken cancellationToken)
        {
            Person person = await _personRepository.GetByCodeNoTracking(userInputViewModel.PersonCode, cancellationToken);
            if (person == null)
                throw new BadRequestException(" این شخص وجود ندارد");


            User user = await _userRepository.GetUserByPersonCode(userInputViewModel.PersonCode, cancellationToken);
            if (user != null)
            {
                throw new BadRequestException(" این شخص دارای حساب کاربری می باشد و نمی توان حساب جدیدی درج کرد");
            }

            User oldUser = await _userRepository.GetByUserName(userInputViewModel.Username);
            if (oldUser != null)
            {
                throw new BadRequestException("این نام کاربری قبلا ثبت شده است");
            }


            user = new User
            {
                Code = Guid.NewGuid(),
                Username = userInputViewModel.Username,
                Password = SecurityHelper.GetSha256Hash(userInputViewModel.Password),
                Email = userInputViewModel.Email,
                PersonId = person.Id,
                IsDeleted = false
            };
            await _userRepository.AddAsync(user, cancellationToken);

            UserResultViewModel result = _mapper.Map<UserResultViewModel>(user);

            result.PersonCode = person.Code;
            result.Person = _mapper.Map<UserPersonViewModel>(person);

            return result;
        }

        public async Task<User> GetVelidUserCommon(string username, Guid code, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByCode(code, cancellationToken);
            if (user == null)
                throw new BadRequestException(" کاربر با این کد وجود ندارد");

            User oldUser = await _userRepository.GetByUserName(username);
            if (oldUser != null)
            {
                if (oldUser.Code == code)
                {
                    return user;
                }
                else
                {
                    throw new BadRequestException("این نام کاربری تکراری است");
                }
            }

            return user;
        }

        public async Task<UserResultViewModel> Update(Guid userCode, UserUpdateInputViewModel userViewModel, CancellationToken cancellationToken)
        {
            User user = await GetVelidUserCommon(userViewModel.Username, userCode, cancellationToken);

                user.Username = userViewModel.Username;
                user.Email = userViewModel.Email;


                await _userRepository.UpdateAsync(user, cancellationToken);
            

            
            return _mapper.Map<UserResultViewModel>(user);
        }

        //public async Task<bool> delete(string code, CancellationToken cancellationToken)
        //{
        //    var user_code = Guid.Parse(code);
        //    var user = await _userRepository.GetUserByCode(user_code, cancellationToken);
        //    if (user == null)
        //        throw new BadRequestException(" چنین کاربری وجود ندارد");

        //    await _userRepository.DeleteAsync(user, cancellationToken);
        //    return true;
        //}

        public async Task<bool> Delete(Guid code, CancellationToken cancellationToken)
        {

            User user = await _userRepository.GetUserByCode(code, cancellationToken);
            if (user == null)
                throw new BadRequestException("کد کاربر وجود ندارد");

            await _userRepository.DeleteAsync(user, cancellationToken);

            return true;
        }

        public Task<PagedResult<User>> all(int? page, int? pageSize, CancellationToken cancellationToken)
        {
            int pageNotNull = page ?? _pagingSettings.DefaultPage;
            int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
            var companies = _userRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
            return companies;
        }

        public async Task<List<UserInfoViewModel>> GetData(string parameter, CancellationToken cancellationToken)
        {
            DateTime? date;
            switch (parameter)
            {
                case "daily":
                    date = DateTime.Now;
                    break;
                case "weekly":
                    date = DateTime.Now.AddDays(-7);
                    break;
                default:
                    date = null;
                    break;
            }

            var userList = await _userRepository.GetData(date, cancellationToken);
            var model = _mapper.Map<List<UserInfoViewModel>>(userList);
            return model;
        }
        public async Task<UserInfoViewModel> AssignUserRole(Guid code, long role_id, CancellationToken cancellationToken)
        {

            User user = await _userRepository.GetUserByCode(code, cancellationToken);
            if (user == null)
                throw new BadRequestException("کاربر وجود ندارد");
            Role role = await _roleRepository.GetByIdAsync(cancellationToken, role_id);
            if (role == null)
                throw new BadRequestException("نقش وجود ندارد");

            UserRole userRole = await _userRoleRepository.GetUserRole(user.Id, role.Id, cancellationToken);
            if (userRole != null)
                throw new BadRequestException("این نقش برای کاربر وجود دارد");
            user.UserRoles.Add(new UserRole()
            {
                RoleId = role_id
            });
            await _userRepository.UpdateAsync(user, cancellationToken);
            return _mapper.Map<UserInfoViewModel>(user);
        }
        public async Task<UserInfoViewModel> DeleteUserRole(Guid code, long role_id, CancellationToken cancellationToken)
        {

            User user = await _userRepository.GetUserByCode(code, cancellationToken);
            if (user == null)
                throw new BadRequestException("کاربر وجود ندارد");
            Role role = await _roleRepository.GetByIdAsync(cancellationToken, role_id);
            if (role == null)
                throw new BadRequestException("نقش وجود ندارد");

            UserRole userRole = await _userRoleRepository.GetUserRoleTracking(user.Id, role.Id, cancellationToken);
            if (userRole == null)
                throw new BadRequestException("این نقش برای کاربر وجود ندارد");
            await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
            return _mapper.Map<UserInfoViewModel>(user);
        }

        public async Task<UserInfoViewModel> GetDataMine(long userId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            return _mapper.Map<UserInfoViewModel>(user);
        }

        public async Task<UserInfoViewModel> UpdateMine(long userId, UserViewModel userViewModel, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            user.Username = userViewModel.Username;
            user.Email = userViewModel.Email;
            user.Password = userViewModel.Password;


            await _userRepository.UpdateAsync(user, cancellationToken);
            return _mapper.Map<UserInfoViewModel>(user);
        }

        public async Task<UserResultViewModel> GetUser(Guid userCode, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByCodeWithDetail(userCode, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException("کاربری با این کد وجود ندارد");
            }

            return _mapper.Map<UserResultViewModel>(user);
        }

        public async Task<PagedResult<UserResultViewModel>> GetUsers(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<User> users = await _userRepository.GetAllUsersByCodeWithDetail(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<UserResultViewModel>>(users);
        }

        public async Task<string> ChangeUserPasswordCommon(Guid UserCode, UserChangePasswordViewModel model, CancellationToken cancellationToken)
        {
            User user = await GetValidUserForPasswordChangingCommon(UserCode, model, cancellationToken);

            user.Password = SecurityHelper.GetSha256Hash(model.Password);

            await _userRepository.UpdateAsync(user, cancellationToken);

            return "رمز عبور با موفقیت تغییر یافت";
        }

        public async Task<User> GetValidUserForPasswordChangingCommon(Guid UserCode, UserChangePasswordViewModel model, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByCode(UserCode, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException("کاربری با این کد وجو ندارد");
            }

           

            if (model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException("رمز عبور وارد شده باید با تایید آن برابر باشد ");
            }

            return user;
        }

        public async Task<string> ChangeUserPassword(Guid UserCode, UserChangePasswordViewModel model, CancellationToken cancellationToken)
        {

            return await ChangeUserPasswordCommon(UserCode, model, cancellationToken);
        }

        public async Task<string> ChangeUserPasswordMine(long userId, MineUserChangePasswordViewModel model, CancellationToken cancellationToken)
        {
            User currentUser = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (currentUser == null)
            {
                throw new BadRequestException("شما در این سیستم نقشی ندارید");
            }

            if (currentUser.Password != SecurityHelper.GetSha256Hash(model.OldPassword))
            {
                throw new BadRequestException("رمز عبور فعلی را اشتباه وارد کردید");
            }

            if (model.Password != model.ConfirmPassword)
            {
                throw new BadRequestException("رمز عبور وارد شده باید با تایید آن برابر باشد ");
            }

            currentUser.Password = SecurityHelper.GetSha256Hash(model.Password);

            await _userRepository.UpdateAsync(currentUser, cancellationToken);

            return "رمز عبور با موفقیت تغییر یافت";
        }
    }
}
