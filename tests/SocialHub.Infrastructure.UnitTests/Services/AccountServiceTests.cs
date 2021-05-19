using EntityFrameworkCoreMock.NSubstitute;
using NSubstitute;
using SocialHub.Application.Interfaces;
using SocialHub.Domain.Entities;
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

            _cryptographyService.Hash(default)
                .ReturnsForAnyArgs("");
        }

        [Fact]
        public async Task AddAccountAsync_ShouldCallHash_WhenCalled()
        {
            // Arrange
            var testAccount = new Account();

            // Act 
            await _sut.AddAccountAsync(testAccount);

            // Assert
            _cryptographyService.Received(1).Hash(default);
        }
    }
}
