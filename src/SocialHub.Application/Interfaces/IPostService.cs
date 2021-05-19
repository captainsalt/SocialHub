using LanguageExt;
using LanguageExt.Common;
using System;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface IPostService
    {
        Task<Either<Error, Unit>> CreatePost(Guid authorId, string content);

        Task<Either<Error, Unit>> LikePost(Guid accountId, Guid postId);

        Task<Either<Error, Unit>> RemoveLike(Guid accountId, Guid postId);

        Task<Either<Error, Unit>> SharePost(Guid accountId, Guid postId);

        Task<Either<Error, Unit>> RemoveShare(Guid accountId, Guid postId);
    }
}
