using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Either<Error, Post>> CreatePostAsync(Guid authorId, string content)
        {
            var accountComputation = await _accountService.GetAccountByIdAsync(authorId);

            return await accountComputation.MatchAsync(
                async acc =>
                {
                    return await _dbContext.AddAsync(new Post(acc.Id, content))
                        .ToAsync()
                        .Match<Either<Error, Post>>(
                            post => post,
                            err => err
                        );
                },
                err => err);
        }

        public EitherAsync<Error, List<Post>> GetHomeFeed(Guid accountId)
        {
            return
                _accountService.GetAccountByIdAsync(accountId)
                    .ToAsync()
                    .Bind(acc =>
                    {
                        var sharedContext = GetSharedPosts(acc).ToAsync();
                        var likesContext = GetSharedPosts(acc).ToAsync();
                        var followedAccountContext = GetFollowedAccountPosts(acc).ToAsync();

                        return sharedContext.Bind(shared =>
                            likesContext.Bind(likes =>
                            followedAccountContext.Bind<List<Post>>(followed =>
                            {
                                return shared.ConcatFast(likes)
                                        .ConcatFast(followed)
                                        .ToList();
                            })));
                    });
        }

        // TODO: Remove code repetition
        // TODO: Use bind
        public async Task<Either<Error, Unit>> LikePostAsync(Guid accountId, Guid postId)
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

        public async Task<Either<Error, Unit>> SharePostAsync(Guid accountId, Guid postId)
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

        public async Task<Either<Error, Unit>> RemoveLikeAsync(Guid accountId, Guid postId)
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

        public async Task<Either<Error, Unit>> RemoveShareAsync(Guid accountId, Guid postId)
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

        private async Task<Either<Error, List<Post>>> GetSharedPosts(Account account)
        {
            var sharesCollection = _dbContext.Entry(account).Collection(nameof(account.Shares));

            if (!sharesCollection.IsLoaded)
                await sharesCollection.LoadAsync();

            return account.Shares;
        }

        private async Task<Either<Error, List<Post>>> GetLikedPosts(Account account)
        {
            var likedCollection = _dbContext.Entry(account).Collection(nameof(account.Likes));

            if (!likedCollection.IsLoaded)
                await likedCollection.LoadAsync();

            return account.Likes;
        }

        private async Task<Either<Error, List<Post>>> GetFollowedAccountPosts(Account account)
        {
            var followedAccountPosts = _dbContext.Entry(account).Collection(nameof(account.Following));

            if (!followedAccountPosts.IsLoaded)
                await followedAccountPosts.LoadAsync();

            return account.Following
                .SelectMany(acc => acc.Posts.ConcatFast(acc.Shares))
                .ToList();
        }
    }
}
