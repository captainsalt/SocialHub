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
        public async Task CreatePostAsync_ShouldReturnRightAsync_WhenAccountExists()
        {
            // Arrange 
            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.CreatePostAsync(_guid, "");

            // Assert
            result.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task CreatePostAsync_ShouldReturnLeftAsync_WhenAccountDoesNotExist()
        {
            // Arrange
            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.CreatePostAsync(_guid, "");

            // Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task LikePostAsync_ShouldReturnRightAsync_WhenPostExists()
        {
            // Arrange 
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.LikePostAsync(_guid, _guid);

            //Assert
            result.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task LikePostAsync_ShouldReturnLeftAsync_WhenPostDoesNotExist()
        {
            // Arrange 
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(ValueTask.FromResult<Post>(null));

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.LikePostAsync(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task LikePostAsync_ShouldReturnLeftAsync_WhenAccountDoesNotExist()
        {
            // Arrange 
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.LikePostAsync(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task SharePostAsync_ShouldReturnRightAsync_WhenPostExists()
        {
            // Arrange
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.SharePostAsync(_guid, _guid);

            //Assert
            result.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task SharePostAsync_ShouldReturnErrorAsync_WhenPostDoestNotExist()
        {
            // Arrange
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(ValueTask.FromResult<Post>(null));

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(_testUser);

            // Act 
            var result = await _sut.SharePostAsync(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task SharePostAsync_ShouldReturnErrorAsync_WhenAccountDoesNotExist()
        {
            // Arrange
            _dbContext.Object
                .Posts
                .FindAsync(default)
                .ReturnsForAnyArgs(_testPost);

            _accountService.GetAccountByIdAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.SharePostAsync(_guid, _guid);

            //Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldLoadLikesCollectionAsync_WhenCalled()
        {

        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldReturnRightAsync_WhenPostExists()
        {

        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldReturnErrorAsync_WhenPostDoesNotExist()
        {

        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldReturnErrorAsync_WhenAccountDoesNotExit()
        {

        }

        [Fact]
        public async Task RemoveShareAsync_ShouldLoadSharesCollectionAsync_WhenCalled()
        {

        }

        [Fact]
        public async Task RemoveShareAsync_ShouldReturnRightAsync_WhenPostExists()
        {
        }

        [Fact]
        public async Task RemoveShareAsync_ShouldReturnErrorAsync_WhenPostDoesNotExist()
        {

        }

        [Fact]
        public async Task RemoveShareAsync_ShouldReturnErrorAsync_WhenAccountDoesNotExist()
        {

        }
    }
}
