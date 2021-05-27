using EntityFrameworkCoreMock.NSubstitute;
using FluentAssertions;
using LanguageExt.Common;
using NSubstitute;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
using SocialHub.Infrastructure.Services;
using SocialHub.Infrastructure.Tests.Helpers;
using System;
using System.Collections.Generic;
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
        public async Task CreatePostAsync_ShouldReturnRight_WhenAccountExists()
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
        public async Task CreatePostAsync_ShouldReturnLeft_WhenAccountDoesNotExist()
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
        public async Task LikePostAsync_ShouldReturnRight_WhenPostExists()
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
        public async Task LikePostAsync_ShouldReturnLeft_WhenPostDoesNotExist()
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
        public async Task LikePostAsync_ShouldReturnLeft_WhenAccountDoesNotExist()
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
        public async Task SharePostAsync_ShouldReturnRight_WhenPostExists()
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
        public async Task SharePostAsync_ShouldReturnLeft_WhenPostDoestNotExist()
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
        public async Task SharePostAsync_ShouldReturnLeft_WhenAccountDoesNotExist()
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
        public async Task RemoveLikeAsync_ShouldLoadLikesCollection_WhenCalled()
        {

        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldReturnRight_WhenPostExists()
        {

        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldReturnLeft_WhenPostDoesNotExist()
        {

        }

        [Fact]
        public async Task RemoveLikeAsync_ShouldReturnLeft_WhenAccountDoesNotExit()
        {

        }

        [Fact]
        public async Task RemoveShareAsync_ShouldLoadSharesCollection_WhenCalled()
        {

        }

        [Fact]
        public async Task RemoveShareAsync_ShouldReturnRight_WhenPostExists()
        {
        }

        [Fact]
        public async Task RemoveShareAsync_ShouldReturnLeft_WhenPostDoesNotExist()
        {

        }

        [Fact]
        public async Task RemoveShareAsync_ShouldReturnLeft_WhenAccountDoesNotExist()
        {

        }
    }
}
