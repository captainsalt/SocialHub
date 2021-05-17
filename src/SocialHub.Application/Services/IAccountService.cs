using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Models;
using SocialHub.Domain.Models;
using System.Threading.Tasks;

namespace SocialHub.Application.Services
{
    public interface IAccountService
    {
        Task<Option<Account>> GetUserByIDAsync(int id);

        Task<Option<Account>> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Register user to database then returns a JTW
        /// </summary>
        /// <param name="user"></param>
        /// <returns>An exception or a JWT</returns>
        Task<Either<Error, AuthResponse>> RegisterAsync(RegisterRequest request);
    }
}
