using Core.ValueObjects;

namespace Core.Test.ValueObjects;

public class UserNameTest
{
    [Fact]
    public void Create_ValidUserName_Success()
    {
        // Arrange
        const string input = "test";
        // Act
        var result = UserName.Create(input);
        // Assert
        Assert.NotNull(result);
        Assert.Equal(input, result.Data!.Value);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Create_EmptyUserName_Failure()
    {
        // Arrange
        const string input = "";
        // Act
        var result = UserName.Create(input);
        //Asset
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("    a    ")]
    [InlineData(" aa ")]
    public void Create_InvalidUserName_Failure(string input)
    {
        // Arrange & Act
        var result = UserName.Create(input);
        //Asset
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
    }
}
