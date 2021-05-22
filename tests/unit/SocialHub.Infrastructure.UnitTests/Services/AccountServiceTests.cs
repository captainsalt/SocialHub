using EntityFrameworkCoreMock.NSubstitute;
using FluentAssertions;
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
    public class AccountServiceTests
    {
        private readonly AccountService _sut;
        private readonly DbContextMock<TestDbContext> _dbContext = new();
        private readonly ICryptographyService _cryptographyService = Substitute.For<ICryptographyService>();
        private readonly Account _testAccount = new();
        private readonly Guid _guid = new();

        public AccountServiceTests()
        {
            _sut = new AccountService(_dbContext.Object, _cryptographyService);

            _cryptographyService.Hash(default)
                .ReturnsForAnyArgs("");
        }

        [Fact]
        public async Task AddAccountAsync_ShouldCallHash_WhenCalled()
        {
            // Act 
            await _sut.AddAccountAsync(_testAccount);

            // Assert
            _cryptographyService.Received(1).Hash(default);
        }

        [Fact]
        public async Task FollowAccountAsync_ShouldReturnLeft_WhenFollowingSelf()
        {
            // Act 
            var isLeft = await _sut.FollowAccountAsync(_guid, _guid).IsLeft;

            // Assert
            isLeft.Should().BeTrue();
        }

        [Fact]
        async Task FollowAccountAsync_ShouldReturnLeft_WhenAlreadyFollowing()
        {

        }
    }
}
