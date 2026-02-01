using Core.ValueObjects;

namespace Core.Test.ValueObjects;

public class PasswordTest
{
    [Fact]
    public void Create_ValidPassword_Success()
    {
        // Arrange
        const string input = "123456";
        // Act
        var result = Password.Create(input);
        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(input, result.Data!.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("      ")]
    [InlineData("a")]
    [InlineData("   a    ")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    public void Create_InvalidPassword_Success(string input)
    {
        // Arrange & Act
        var result = Password.Create(input);
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
    }
}
