﻿using LanguageExt;
using LanguageExt.Common;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                from acc in _accountService.GetAccountByIdAsync(accountId).ToAsync()
                from shared in GetSharedPosts(acc).ToAsync()
                from own in GetOwnPosts(acc).ToAsync()
                from followed in GetFolloweePosts(acc).ToAsync()
                select shared.ConcatFast(own).ConcatFast(followed).Distinct().ToList();
        }

        public EitherAsync<Error, List<Post>> GetProfileFeed(Guid accountId)
        {
            return
                from acc in _accountService.GetAccountByIdAsync(accountId).ToAsync()
                from shared in GetSharedPosts(acc).ToAsync()
                from own in GetOwnPosts(acc).ToAsync()
                select shared.ConcatFast(own).Distinct().ToList();
        }

        // TODO: Remove code repetition
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
                        .Collection(nameof(res.account.Likes))
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
                        .Collection(nameof(res.account.Shares))
                        .LoadAsync();

                    res.account.Shares.Remove(res.post);
                    await _dbContext.UpdateAsync(res.post);

                    return unit;
                },
                err => err
            );
        }

        private async Task<Either<Error, List<Post>>> GetOwnPosts(Account account)
        {
            var postCollection = _dbContext.Entry(account).Collection(nameof(account.Posts));

            if (!postCollection.IsLoaded)
                await postCollection.LoadAsync();

            return account.Posts;
        }

        private async Task<Either<Error, List<Post>>> GetSharedPosts(Account account)
        {
            var sharesCollection = _dbContext.Entry(account).Collection(nameof(account.Shares));

            if (!sharesCollection.IsLoaded)
                await sharesCollection.LoadAsync();

            return account.Shares;
        }

        /// <summary>
        /// Gets the followees authored and shared posts 
        /// </summary>
        /// <param name="account">Followee</param>
        /// <returns></returns>
        private async Task<Either<Error, List<Post>>> GetFolloweePosts(Account account)
        {
            var followees = _dbContext.Entry(account).Collection(nameof(account.Following));

            if (!followees.IsLoaded)
                await followees.LoadAsync();

            return account.Following
                .SelectMany(followee => followee.Posts.ConcatFast(followee.Shares))
                .ToList();
        }
    }
}
