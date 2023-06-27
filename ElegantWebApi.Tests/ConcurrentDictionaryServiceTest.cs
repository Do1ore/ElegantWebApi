using ElegantWebApi.Infrastructure.Contracts;
using ElegantWebApi.Infrastructure.Services;

namespace ElegantWebApi.Tests;

public class ConcurrentDictionaryServiceTest
{
    private readonly IConcurrentDictionaryService _dictionaryService;

    public ConcurrentDictionaryServiceTest()
    {
        _dictionaryService = new ConcurrentDictionaryService();
    }

    [Fact]
    public void Create_ReturnsCompletedTask()
    {
        
        //Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";
        var listToAdd = new List<object> { "value23", 1345 };

        //Act
        var actualResult = _dictionaryService.CreateAsync(id, listToAdd);
        //Assert

        Assert.True(actualResult.IsCompletedSuccessfully);
    }

    [Fact]
    public void Append_ReturnsCompletedTask()
    {
        //Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";
        object valueToAppend = "object value";

        //Act
        var actualResult = _dictionaryService.AppendAsync(id, valueToAppend);

        //Assert
        Assert.True(actualResult.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task CreateAndGet_ReturnsListOfObject()
    {
        //Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";
        var expectedResult = new List<object> { "value23", 1345 };
        //Act
        var actualResultOfCreate = _dictionaryService.CreateAsync(id, expectedResult);
        var actualResultOfGet = await _dictionaryService.GetAsync(id);

        //Assert
        Assert.True(actualResultOfCreate.IsCompletedSuccessfully);
        Assert.Equal(expectedResult, actualResultOfGet);
    }

    [Fact]
    public async Task CreateAndDelete_ReturnsListOfObject()
    {
        //Arrange
        var id = "f21c4da1-73b4-41f9-b34b-0aa37143bf6c";
        var expectedResult = new List<object> { "value23", 1345 };
        //Act
        var actualResultOfCreate = _dictionaryService.CreateAsync(id, expectedResult);
        var actualResultOfGet = await _dictionaryService.DeleteAsync(id);

        //Assert
        Assert.True(actualResultOfCreate.IsCompletedSuccessfully);
        Assert.Equal(expectedResult, actualResultOfGet);
    }
}