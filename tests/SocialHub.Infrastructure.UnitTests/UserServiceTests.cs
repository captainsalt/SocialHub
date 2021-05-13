using SocialHub.Application.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using SocialHub.Domain.Models;
using FluentAssertions;
using SocialHub.Domain;

namespace SocialHub.Infrastructure.Tests
{
    public class UserServiceTests
    {
        private readonly AccountService _sut;
        private readonly ISocialHubDbContext _dbContext = Substitute.For<ISocialHubDbContext>();

        public UserServiceTests()
        {
            _sut = new AccountService(_dbContext);
        }

        [Fact]
        public async Task GetUserByID_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var testUser = new Account("Jimmy", "1111");

            _dbContext.Accounts
                .FindAsync(Arg.Any<int>())
                .Returns(testUser);

            // Act
            var user = await _sut.GetUserByID(1);

            // Assert
            user.IsSome.Should().BeTrue();
        }

        [Fact]
        public async Task GetUserByID_ShouldReturnNone_WhenUserDoesNotExist()
        {
            // Arrange
            var user = await _sut.GetUserByID(1);


            user.IsNone.Should().BeTrue();
        }
    }
}
