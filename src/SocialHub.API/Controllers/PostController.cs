using AutoMapper;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using SocialHub.API.Attributes;
using SocialHub.API.Dtos;
using SocialHub.API.Models;
using SocialHub.API.Models.Dtos;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.API.Controllers
{
    [Route("api/[controller]")]
    [JwtAuth]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IJwtService _jwtService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public PostController(
            IPostService postService,
            IJwtService jwtService,
            IAccountService accountService,
            IMapper mapper)
        {
            _postService = postService;
            _jwtService = jwtService;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetHomeFeed()
        {
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from feed in _postService.GetHomeFeed(tokenAccount.Id)
                select feed;

            return await result.Match<IActionResult>(
                posts => Ok(MapPostList(posts)),
                err => BadRequest(err)
            );
        }

        [HttpGet("feed/{username}")]
        public async Task<IActionResult> GetProfileFeed(string username)
        {
            var result =
                from acc in _accountService.GetAccountByUsernameAsync(username).ToAsync()
                from feed in _postService.GetHomeFeed(acc.Id)
                select feed;

            return await result.Match<IActionResult>(
                posts => Ok(MapPostList(posts)),
                err => BadRequest(err)
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from post in _postService.CreatePostAsync(tokenAccount.Id, request.Content).ToAsync()
                select post;

            return await result.Match<IActionResult>(
                post => Created(HttpContext.Request.Path.Value, MapPost(post)),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikePost([FromQuery] Guid postId)
        {
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from unit in _postService.LikePostAsync(tokenAccount.Id, postId).ToAsync()
                select unit;

            return await result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("share")]
        public async Task<IActionResult> SharePost([FromQuery] Guid postId)
        {
            var result =
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync()
                from unit in _postService.SharePostAsync(tokenAccount.Id, postId).ToAsync()
                select unit;

            return await result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        [HttpDelete("share/remove")]
        public async Task<IActionResult> RemoveShare([FromQuery] Guid postId)
        {
            var result = 
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync() 
                from unit in _postService.RemoveShareAsync(tokenAccount.Id, postId).ToAsync() 
                select unit;

            return await result.Match<IActionResult>(
                unit => Ok(), 
                err => BadRequest(MapError(err))
            );
        }

        [HttpDelete("like/remove")]
        public async Task<IActionResult> RemoveLike([FromQuery] Guid postId)
        {
            var result = 
                from tokenAccount in _jwtService.GetAccountFromToken(HttpContext).ToAsync() 
                from unit in _postService.RemoveLikeAsync(tokenAccount.Id, postId).ToAsync() 
                select unit;

            return await result.Match<IActionResult>(
                unit => Ok(), 
                err => BadRequest(MapError(err))
            );
        }

        private PostDto MapPost(Post post) =>
            _mapper.Map<PostDto>(post);

        private List<PostDto> MapPostList(List<Post> posts) =>
            posts.Select(p => _mapper.Map<PostDto>(p)).ToList();

        private ErrorDto MapError(Error err) =>
            _mapper.Map<ErrorDto>(err);
    }
}
