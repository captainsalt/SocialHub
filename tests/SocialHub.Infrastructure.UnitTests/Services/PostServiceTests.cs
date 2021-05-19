using EntityFrameworkCoreMock.NSubstitute;
using FluentAssertions;
using LanguageExt.Common;
using NSubstitute;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
using SocialHub.Infrastructure.Services;
using SocialHub.Infrastructure.Tests.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SocialHub.Infrastructure.Tests.Services
{
    public class PostServiceTests
    {
        private readonly PostService _sut;
        private readonly DbContextMock<TestDbContext> _dbContext = new();
        private readonly IAccountService _accountService = Substitute.For<IAccountService>();
        private readonly Account _testUser = new();
        private readonly Guid _guid = new();
        private readonly Post _testPost = new();

        public PostServiceTests()
        {
            _sut = new PostService(_dbContext.Object, _accountService);
        }

        [Fact]
        public async Task CreatePost_ShouldReturnRight_WhenAccountExists()
        {
            // Arrange 
            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.CreatePost(_guid, "");

            // Assert
            result.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task CreatePost_ShouldReturnLeft_WhenAccountDoesNotExist()
        {
            // Arrange
            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.CreatePost(_guid, "");

            // Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task LikePost_ShouldReturnRight_WhenPostExists()
        {
            // Arrange 
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.LikePost(_guid, _guid);

            //Assert
            result.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task LikePost_ShouldReturnLeft_WhenPostDoesNotExist()
        {
            // Arrange 
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(ValueTask.FromResult<Post>(null));

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.LikePost(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task LikePost_ShouldReturnLeft_WhenAccountDoesNotExist()
        {
            // Arrange 
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.LikePost(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task SharePost_ShouldReturnRight_WhenPostExists()
        {
            // Arrange
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.SharePost(_guid, _guid);

            //Assert
            result.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task SharePost_ShouldReturnError_WhenPostDoestNotExist()
        {
            // Arrange
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(ValueTask.FromResult<Post>(null));

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.SharePost(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task SharePost_ShouldReturnError_WhenAccountDoesNotExist()
        {
            // Arrange
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.SharePost(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveLike_ShouldLoadLikesCollection_WhenCalled()
        {

        }

        [Fact]
        public async Task RemoveLike_ShouldReturnRight_WhenPostExists()
        {

        }

        [Fact]
        public async Task RemoveLike_ShouldReturnError_WhenPostDoesNotExist()
        {

        }

        [Fact]
        public async Task RemoveLike_ShouldReturnError_WhenAccountDoesNotExit()
        {

        }

        [Fact]
        public async Task RemoveShare_ShouldLoadSharesCollection_WhenCalled()
        {

        }

        [Fact]
        public async Task RemoveShare_ShouldReturnRight_WhenPostExists()
        {
        }

        [Fact]
        public async Task RemoveShare_ShouldReturnError_WhenPostDoesNotExist()
        {

        }

        [Fact]
        public async Task RemoveShare_ShouldReturnError_WhenAccountDoesNotExist()
        {

        }
    }
}
