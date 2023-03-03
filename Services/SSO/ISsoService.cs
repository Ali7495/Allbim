using DAL.Models;
using Models.Login;
using Models.Person;
using Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface ISsoService
    {

        Task<bool> Validate(LoginViewModel loginViewModel);
        Task<User> Register(RegisterViewModel registerViewModel, CancellationToken cancellationToken);
        Task<UserResultViewModel> RegisterFromApi(RegisterViewModel registerViewModel, CancellationToken cancellationToken);
        Task<string> Login(LoginViewModel loginViewModel, CancellationToken cancellationToken);
        Task<string> Login_Dashboard(LoginViewModel loginViewModel, CancellationToken cancellationToken);
        Task<string> RefreshTokenAsync(User user);
        Task<string> GenerateAsync(User user);
        Task<string> VerificationCode(VerificationViewModel viewModel, CancellationToken cancellationToken);
        bool SendEmail(SendSmsViewModel viewModel, CancellationToken cancellationToken);
        Task<string> CheckPhone(string username, string ip, CancellationToken cancellationToken);
        Task<string> TwoStep(string username, CancellationToken cancellationToken);

        Task<string> CheckTwoStep(CheckVerificationViewModel model, CancellationToken cancellationToken);
        Task<string> CheckVerification(CheckVerificationViewModel model, CancellationToken cancellationToken);
        Task<string> ChangePassword(ChangePasswordViewModel mdoel,string username, CancellationToken cancellationToken);

        Task<UserInfoViewModel> GetUserInfo(long userId, CancellationToken cancellationToken);
    }
}
