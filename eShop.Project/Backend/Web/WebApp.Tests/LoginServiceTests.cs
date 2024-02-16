using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Routing;

namespace WebApp.Tests;

public class LoginServiceTests
{
    //private readonly Mock<ILogger<LoginService>> _mockLogger;
    //private readonly LoginService _service;

    //public LoginServiceTests()
    //{
    //    _mockLogger = new Mock<ILogger<LoginService>>();
    //    _service = new LoginService(_mockLogger.Object);
    //}

    [Fact]
    public async Task SignInAsync_Successful()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LoginService>>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockUserInfoResponse = new Mock<UserInfoResponse>();
        var mockTokenResponse = new Mock<TokenResponse>();
        var loginService = new LoginService(mockLogger.Object);

        // Act
        await loginService.SignInAsync(mockHttpContext.Object, mockUserInfoResponse.Object, mockTokenResponse.Object);

        // Assert
        mockLogger.Verify(x => x.LogInformation("SignInAsync started."), Times.Once);
        mockLogger.Verify(x => x.LogInformation("SignInAsync completed."), Times.Once);
    }

    [Fact]
    public async Task SignInAsync_Failure()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LoginService>>();
        var mockHttpContext = new Mock<HttpContext>();
        var mockUserInfoResponse = new Mock<UserInfoResponse>();
        var mockTokenResponse = new Mock<TokenResponse>();
        var loginService = new LoginService(mockLogger.Object);

        mockHttpContext.Setup(x => x.SignInAsync(It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>())).Throws(new Exception());

        // Act & Assert
        Assert.ThrowsAsync<Exception>(() => loginService.SignInAsync(mockHttpContext.Object, mockUserInfoResponse.Object, mockTokenResponse.Object));
        mockLogger.Verify(x => x.LogError(It.IsAny<Exception>(), "Error in SignInAsync."), Times.Once);
    }

}



