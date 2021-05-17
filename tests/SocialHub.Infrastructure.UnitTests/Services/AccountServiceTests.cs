using EntityFrameworkCoreMock.NSubstitute;
using NSubstitute;
using SocialHub.Application.Services;
using SocialHub.Domain.Models;
using SocialHub.Infrastructure.Services;
using SocialHub.Infrastructure.Tests.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace SocialHub.Infrastructure.Tests.Services
{
    public class AccountServiceTests
    {
        private readonly AccountService _sut;
        private readonly DbContextMock<TestDbContext> _dbContext = new();
        private readonly ICryptographyService _cryptographyService = Substitute.For<ICryptographyService>();

        public AccountServiceTests()
        {
            _sut = new AccountService(_dbContext.Object, _cryptographyService);
            _cryptographyService.Hash(default).ReturnsForAnyArgs("");
        }

        [Fact]
        public async Task AddAccountAsync_ShouldCallPassowrdHasher()
        {
            // Arrange
            var expextedArgument = "unhashedPassword";
            var testAccount = new Account() { Password = expextedArgument };

            // Act 
            await _sut.AddAccountAsync(testAccount);

            // Assert
            _cryptographyService.Received(1).Hash(testAccount.Password);
        }
    }
}
