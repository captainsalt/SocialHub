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
using System.Threading.Tasks;

namespace SocialHub.API.Controllers
{
    [Route("api/[controller]")]
    [JwtAuth]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IJwtService jwtService, IMapper mapper)
        {
            _postService = postService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            var account = _jwtService.GetAccountFromToken(HttpContext);
            var result = await _postService.CreatePostAsync(account.Id, request.Content);

            return result.Match<IActionResult>(
                post => Created(HttpContext.Request.Path.Value, MapPost(post)),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikePost([FromQuery] Guid postId)
        {
            var account = _jwtService.GetAccountFromToken(HttpContext);
            var result = await _postService.LikePostAsync(account.Id, postId);

            return result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        [HttpPost("share")]
        public async Task<IActionResult> SharePost([FromQuery] Guid postId)
        {
            var account = _jwtService.GetAccountFromToken(HttpContext);
            var result = await _postService.SharePostAsync(account.Id, postId);

            return result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        [HttpDelete("share/remove")]
        public async Task<IActionResult> RemoveShare([FromQuery] Guid postId)
        {
            var account = _jwtService.GetAccountFromToken(HttpContext);
            var result = await _postService.RemoveShareAsync(account.Id, postId);

            return result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        [HttpDelete("like/remove")]
        public async Task<IActionResult> RemoveLike([FromQuery] Guid postId)
        {
            var account = _jwtService.GetAccountFromToken(HttpContext);
            var result = await _postService.RemoveLikeAsync(account.Id, postId);

            return result.Match<IActionResult>(
                unit => Ok(),
                err => BadRequest(MapError(err))
            );
        }

        private PostDto MapPost(Post post) =>
            _mapper.Map<PostDto>(post);

        private ErrorDto MapError(Error err) =>
            _mapper.Map<ErrorDto>(err);
    }
}
