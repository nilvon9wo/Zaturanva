using Zaturanva.Common.ChessBoard;

namespace Zaturanva.Common.Tests.ChessBoard;

public class CoordinatesTests
{
	[Fact]
	public void ImplicitConversionFromString_ShouldCreateCorrectCoordinates()
	{
		// Arrange
		const string coordinateString = "A1";

		// Act
		Coordinates coordinates = coordinateString;

		// Assert
		Assert.Equal(0, coordinates.X);
		Assert.Equal(0, coordinates.Y);
	}

	[Fact]
	public void
		ImplicitConversionToString_ShouldCreateCorrectStringRepresentation()
	{
		// Arrange
		Coordinates coordinates = new(2, 3);

		// Act
		string coordinateString = coordinates;

		// Assert
		Assert.Equal("C4", coordinateString);
	}

	[Fact]
	public void ToCoordinates_ShouldReturnSameCoordinates()
	{
		// Arrange
		Coordinates coordinates = new(5, 7);

		// Act
		Coordinates result = coordinates.ToCoordinates();

		// Assert
		Assert.Equal(coordinates, result);
	}

	[Theory]
	[InlineData("D2", 3, 1)]
	[InlineData("H5", 7, 4)]
	public void ImplicitConversionFromString_ShouldCreateCorrectCoordinates2(
		string coordinateString,
		int expectedX,
		int expectedY
	)
	{
		// Act
		Coordinates coordinates = coordinateString;

		// Assert
		Assert.Equal(expectedX, coordinates.X);
		Assert.Equal(expectedY, coordinates.Y);
	}
}