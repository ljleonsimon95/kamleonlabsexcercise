using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Responses;

namespace TestingNewService;

public class ResponseValidatorTests
{
    [Fact]
    public void Constructor_ValidData_ReturnsValidObject()
    {
        // Arrange
        var newSource = new New { Id = 1, Title = "Título", Body = "Cuerpo", Image = "imagen en base64" };

        // Act
        var response = new SingleNewFullResponse(newSource);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(newSource.Id, response.Id);
        Assert.Equal(newSource.Title, response.Tittle);
        Assert.Equal(newSource.Body, response.Body);
        Assert.NotNull(response.ImageUrl);
    }
}