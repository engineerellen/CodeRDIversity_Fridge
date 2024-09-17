using Domain.Models;
using Moq;
using Xunit;
using Microsoft.Extensions.Configuration;
using Services;
using System.IdentityModel.Tokens.Jwt;


namespace GeladeiraTeste.ServicesTest
{
    public class LoginServiceTest
    {
        [Fact]
        public async Task GenerateJwtToken_Sucesso()
        {

            // Arrange
            var mockConfigSection = new Mock<IConfigurationSection>();
            mockConfigSection.Setup(x => x.Value).Returns("batatinha quando nasce espalha rama pelo chão menininha quando dorme");

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("Key")).Returns(mockConfigSection.Object);

            var loginService = new LoginServices(mockConfig.Object);
            var username = "testuser";

            // Act
            var token = loginService.GenerateJwtToken(username);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.NotNull(jwtToken);
            Assert.Equal(username, jwtToken.Subject);
        }

        [Fact]
        public async Task GenerateJwtToken_StringEmpty_Sucesso()
        {
            // Arrange
            var mockConfigSection = new Mock<IConfigurationSection>();
            mockConfigSection.Setup(x => x.Value).Returns((string?)null);

            var mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(x => x.GetSection("Key")).Returns(mockConfigSection.Object);

            var loginService = new LoginServices(mockConfig.Object);
            var username = "testuser";

            // Act
            var token = loginService.GenerateJwtToken(username);

            // Assert
            Assert.Equal(string.Empty, token);
        }
    }
}
