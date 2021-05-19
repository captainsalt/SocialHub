using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace SocialHub.Infrastructure.Services
{
    public class PostService : IPostService
    {
        private readonly ISocialHubDbContext _dbContext;
        private readonly IAccountService _accountService;

        public PostService(ISocialHubDbContext dbContext, IAccountService accountService)
        {
            _dbContext = dbContext;
            _accountService = accountService;
        }

        public async Task<Either<Error, Post>> GetPostByIdAsync(Guid id)
        {
            var post = await _dbContext.Posts.FindAsync(id);

            if (post is null)
                return Errors.PostNotFound;

            return post;
        }

        public async Task<Either<Error, Unit>> CreatePost(Guid authorId, string content)
        {
            var accountComputation = await _accountService.GetAccountByIdAsync(authorId);

            return await accountComputation.MatchAsync<Either<Error, Unit>>(
                async acc =>
                {
                    await _dbContext.AddAsync(new Post(acc.Id, content));
                    return unit;
                },
                err => err);
        }

        // TODO: Remove code repitition
        public async Task<Either<Error, Unit>> LikePost(Guid accountId, Guid postId)
        {
            var postComputation = await GetPostByIdAsync(postId);
            var accountComputation = await _accountService.GetAccountByIdAsync(accountId);

            var result =
                from post in postComputation
                from account in accountComputation
                select (post, account);

            return await result.MatchAsync<Either<Error, Unit>>(
                async res =>
                {
                    res.account.Likes.Add(res.post);
                    await _dbContext.UpdateAsync(res.account);
                    return unit;
                },
                err => err
            );
        }

        public async Task<Either<Error, Unit>> SharePost(Guid accountId, Guid postId)
        {
            var postComputation = await GetPostByIdAsync(postId);
            var accountComputation = await _accountService.GetAccountByIdAsync(accountId);

            var result =
                from post in postComputation
                from account in accountComputation
                select (post, account);

            return await result.MatchAsync<Either<Error, Unit>>(
                async res =>
                {
                    res.account.Shares.Add(res.post);
                    await _dbContext.UpdateAsync(res.account);
                    return unit;
                },
                err => err
            );
        }

        public async Task<Either<Error, Unit>> RemoveLike(Guid accountId, Guid postId)
        {
            var postComputation = await GetPostByIdAsync(postId);
            var accountComputation = await _accountService.GetAccountByIdAsync(accountId);

            var result =
                from post in postComputation
                from account in accountComputation
                select (post, account);

            return await result.MatchAsync<Either<Error, Unit>>(
                async res =>
                {
                    await _dbContext.Entry(res.account)
                        .Collection("Likes")
                        .LoadAsync();

                    res.account.Likes.Remove(res.post);
                    await _dbContext.UpdateAsync(res.account);

                    return unit;
                },
                err => err
            );
        }

        public async Task<Either<Error, Unit>> RemoveShare(Guid accountId, Guid postId)
        {
            var postComputation = await GetPostByIdAsync(postId);
            var accountComputation = await _accountService.GetAccountByIdAsync(accountId);

            var result =
                from post in postComputation
                from account in accountComputation
                select (post, account);

            return await result.MatchAsync<Either<Error, Unit>>(
                async res =>
                {
                    await _dbContext.Entry(res.account)
                        .Collection("Shares")
                        .LoadAsync();

                    res.account.Shares.Remove(res.post);
                    await _dbContext.UpdateAsync(res.post);

                    return unit;
                },
                err => err
            );
        }
    }
}
