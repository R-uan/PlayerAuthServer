using System;
using Microsoft.Extensions.Options;
using Moq;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities;

namespace PlayerAuthServer.Tests.UnitTests.Services
{
    public class JwtServiceTests
    {
        private readonly JwtService jwtService;
        private readonly Mock<IOptions<JwtSettings>> mockJwtSettings;

        public JwtServiceTests()
        {
            var settings = new JwtSettings()
            {
                Issuer = "Tester",
                SigningKey = "A3F1B2C490D5E678F12A34B5C6D7E890"
            };

            this.mockJwtSettings = new Mock<IOptions<JwtSettings>>();
            this.mockJwtSettings.Setup(settings => settings.Value).Returns(settings);
            this.jwtService = new JwtService(this.mockJwtSettings.Object);
        }

        [Fact]
        public void ShouldGenerateJwToken()
        {
            Player player = new()
            {
                Email = "player1@example.com",
                Nickname = "PlayerOne",
                PasswordHash = "hash1",
                Decks = []
            };

            var result = this.jwtService.GenerateToken(player);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
