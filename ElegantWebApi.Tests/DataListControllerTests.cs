
using ElegantWebApi.Application.Features.AddDataList;
using ElegantWebApi.Application.Features.DeleteDataList;
using ElegantWebApi.Application.Features.GetDataList;
using ElegantWebApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ElegantWebApi.Tests;

public class DataListControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;

    public DataListControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    [Fact]
    public async Task GetDataListFromIdAsync_ReturnsOkResult()
    {
        // Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";

        var expectedResult = new List<object> { "value", "value1" };

        _mediatorMock.Setup(x => x.Send(It.IsAny<GetDataListCommand>(), CancellationToken.None))
            .ReturnsAsync(expectedResult);
        var controller = new DataListController(_mediatorMock.Object);

        // Act
        var response = await controller.GetDataListFromIdAsync(id);

        // Assert
        Assert.IsType<OkObjectResult>(response);
        var okResult = response as OkObjectResult;
        Assert.Equal(expectedResult, okResult?.Value);
    }

    [Fact]
    public async Task AddKeyValuePair_ReturnsOkResult()
    {
        // Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";
        var expectedDataListModel = new DataListModel
        {
            Id = Guid.Parse(id),
            Values = new List<object> { "line", "stroke" },
            ExpirationTime = DateTime.Now
        };
        var expectedResult = expectedDataListModel;
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddDataListCommand>(), CancellationToken.None))
            .ReturnsAsync(expectedResult);
        var controller = new DataListController(_mediatorMock.Object);

        // Act
        var response = await controller.AddKeyValuePair(expectedDataListModel);

        // Assert
        Assert.IsType<OkObjectResult>(response);
        var okResult = response as OkObjectResult;
        Assert.Equal(expectedResult, okResult?.Value);
    }

    [Fact]
    public async Task AppendValues_ReturnsOkResult()
    {
        // Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";

        var expectedDataListModel = new SingleDataModel
        {
            Id = Guid.Parse(id),
            Value = "single_value",
        };
        var controller = new DataListController(_mediatorMock.Object);

        // Act
        var response = await controller.AppendValues(expectedDataListModel);

        // Assert
        Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public async Task DeleteKeyValuePairAsync_ReturnsOkResult()
    {
        // Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";
        var expectedResult = new DataListModel
        {
            Id = Guid.Parse(id),
            Values = new List<object> { "line", "stroke" },
            ExpirationTime = DateTime.Now
        };
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteRecordListCommand>(), CancellationToken.None))
            .ReturnsAsync(expectedResult);
        var controller = new DataListController(_mediatorMock.Object);

        // Act
        var response = await controller.DeleteKeyValuePairAsync(id);

        // Assert
        Assert.IsType<OkObjectResult>(response);
        var okResult = response as OkObjectResult;
        Assert.Equal(expectedResult, okResult?.Value);
    }
}