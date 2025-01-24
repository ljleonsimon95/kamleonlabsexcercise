using KamaleonlabsExercise.AppDbContext;
using KamaleonlabsExercise.Features.News.Data;
using KamaleonlabsExercise.Features.News.Errors;
using KamaleonlabsExercise.Features.News.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestingNewService;

public class AddNewHandlerTests
{
    [Fact]
    public async Task HandleAsync_AddsNewNewsItemToDbContext()
    {
        // Arrange
        var options = new DbContextOptions<NewsDbContext>();
        var contextMock = new Mock<NewsDbContext>(options);
        var newsMock = new Mock<DbSet<New>>();
        contextMock.Setup(c => c.News).Returns(newsMock.Object);
        var handler = new AddNewHandler(contextMock.Object);
        var input = new AddNewPayload("Test Title", "Test Body");

        // Act
        await handler.HandleAsync(input);

        // Assert
        newsMock.Verify(n => n.AddAsync(It.IsAny<New>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ReturnsAddedNewsItem()
    {
        // Arrange
        var options = new DbContextOptions<NewsDbContext>();
        var contextMock = new Mock<NewsDbContext>(options);
        var newsMock = new Mock<DbSet<New>>();
        contextMock.Setup(c => c.News).Returns(newsMock.Object);
        var handler = new AddNewHandler(contextMock.Object);
        var input = new AddNewPayload("Test Title", "Test Body");

        // Act
        var result = await handler.HandleAsync(input);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(input.Title, result.Title);
        Assert.Equal(input.Body, result.Body);
    }

    [Fact]
    public async Task HandleAsync_NotAllowsNullTittleWhileAdding()
    {
        // Arrange
        var options = new DbContextOptions<NewsDbContext>();
        var contextMock = new Mock<NewsDbContext>(options);
        var newsMock = new Mock<DbSet<New>>();
        contextMock.Setup(c => c.News).Returns(newsMock.Object);
        var handler = new AddNewHandler(contextMock.Object);
        var input = new AddNewPayload(null, "Test Body");

        // Act and Assert
        await Assert.ThrowsAsync<InvalidTittleError>(() => handler.HandleAsync(input));
    }
}