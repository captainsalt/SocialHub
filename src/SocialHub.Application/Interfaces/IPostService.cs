using LanguageExt;
using LanguageExt.Common;
using SocialHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialHub.Application.Interfaces
{
    public interface IPostService
    {
        Task<Either<Error, Post>> CreatePostAsync(Guid authorId, string content);

        Task<Either<Error, Unit>> LikePostAsync(Guid accountId, Guid postId);

        Task<Either<Error, Unit>> RemoveLikeAsync(Guid accountId, Guid postId);

        Task<Either<Error, Unit>> SharePostAsync(Guid accountId, Guid postId);

        Task<Either<Error, Unit>> RemoveShareAsync(Guid accountId, Guid postId);

        EitherAsync<Error, List<Post>> GetHomeFeed(Guid accountId);

        EitherAsync<Error, List<Post>> GetProfileFeed(Guid accountId);

        EitherAsync<Error, List<Post>> PopulatePostStatus(Guid accountId, List<Post> posts);
    }
}
