using FluentAssertions;
using LanguageExt.Common;
using NSubstitute;
using SocialHub.Application.Interfaces;
using SocialHub.Application.Models;
using SocialHub.Domain.Entities;
using SocialHub.Infrastructure.Services;
using System.Threading.Tasks;
using Xunit;

namespace SocialHub.Infrastructure.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly AuthenticationService _sut;
        private readonly ICryptographyService _cryptographyService = Substitute.For<ICryptographyService>();
        private readonly IAccountService _accountService = Substitute.For<IAccountService>();
        private readonly IJwtService _jwtService = Substitute.For<IJwtService>();
        private readonly LoginRequest _loginRequest = new("", "");
        private readonly RegisterRequest _registerRequest = new("", "", "");
        private readonly Account _testAccount = new();

        public AuthenticationServiceTests()
        {
            _sut = new AuthenticationService(_accountService, _cryptographyService, _jwtService);

            _cryptographyService.Hash(default)
                .ReturnsForAnyArgs(call => call.Arg<string>());

            _jwtService.GenerateJwtToken(default)
                .ReturnsForAnyArgs("");
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnRight_WhenAccountExists()
        {
            // Arrange 
            _accountService.GetAccountByUsernameAsync(default)
                .ReturnsForAnyArgs(_testAccount);

            _cryptographyService.IsMatch(default, default)
                .ReturnsForAnyArgs(true);

            // Act
            var response = await _sut.LoginAsync(_loginRequest);

            // Assert 
            response.IsRight.Should().BeTrue();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnLeft_WhenAccountDoesNotExist()
        {
            // Arrange 
            _accountService.GetAccountByUsernameAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act
            var response = await _sut.LoginAsync(_loginRequest);

            // Assert 
            response.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnLeft_WhenPasswordsDoNotMatch()
        {
            // Arrange 
            _accountService.GetAccountByUsernameAsync(default)
                .ReturnsForAnyArgs(_testAccount);

            _cryptographyService.IsMatch(default, default)
                .ReturnsForAnyArgs(false);

            // Act
            var response = await _sut.LoginAsync(_loginRequest);

            // Assert 
            response.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnLeft_WhenAccountExists()
        {
            // Arrange
            _accountService.GetAccountByUsernameAsync(default)
                .ReturnsForAnyArgs(_testAccount);

            // Act 
            var result = await _sut.RegisterAsync(_registerRequest);

            // Assert
            result.IsLeft.Should().BeTrue();
        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnRight_WhenAccountIsCreated()
        {
            // Arrange
            _accountService.GetAccountByUsernameAsync(default)
                .ReturnsForAnyArgs(default(Error));

            // Act 
            var result = await _sut.RegisterAsync(_registerRequest);

            // Assert
            result.IsRight.Should().BeTrue();
        }
    }
}
