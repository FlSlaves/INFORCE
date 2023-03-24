using INFORCE.Models;

namespace INFORCE.Services
{
    public interface IAuthorizeService
    {
        Task<ResponseMessage> SignUp(UserRegParam paramUser);
        Task<ResponseMessage> SignIn(UserLogParam paramUser);
    }
}
