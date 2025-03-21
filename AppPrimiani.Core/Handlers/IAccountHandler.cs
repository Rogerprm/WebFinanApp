using AppPrimiani.Core.Requests.Account;
using AppPrimiani.Core.Responses;

namespace AppPrimiani.Core.Handlers
{
    public interface IAccountHandler
    {
        Task<Response<string>> LoginAsync(LoginRequest request);
        Task<Response<string>> RegisterAsync(RegisterRequest register);
        Task LogoutAsync();
    }
}
