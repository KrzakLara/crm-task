using CRM_Property_Functions;
using Xunit;

public class PropertyDtoTests
{
    [Fact]
    public void PropertyDto_MapsValuesCorrectly()
    {
        // Arrange
        var dto = new PropertyDto
        {
            Name = "Hotel Royal",
            Type = "hotel",
            Location = "Zagreb",
            Rooms = "100",
            Rate = "120.50",
            Rating = "4.5"
        };

        // Assert
        Assert.Equal("Hotel Royal", dto.Name);
        Assert.Equal("hotel", dto.Type);
        Assert.Equal("Zagreb", dto.Location);
        Assert.Equal("100", dto.Rooms);
        Assert.Equal("120.50", dto.Rate);
        Assert.Equal("4.5", dto.Rating);
    }

    [Fact]
    public void PropertyDto_AllowsNullOptionalFields()
    {
        var dto = new PropertyDto
        {
            Name = "Test Hotel"
        };

        Assert.Equal("Test Hotel", dto.Name);
        Assert.Null(dto.Type);
        Assert.Null(dto.Location);
        Assert.Null(dto.Rooms);
        Assert.Null(dto.Rate);
        Assert.Null(dto.Rating);
    }
}
