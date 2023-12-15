using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.ChessBoard.Geometry;

namespace Zaturanva.Common.Tests.ChessBoard.Geometry;

public class SquareCheckerTests
{
	[Fact]
	public void AreSquare_ReturnsTrue_WhenCoordinatesAreInLowerLeftCorner()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1",
			"a2",
			"b1",
			"b2",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void AreSquare_ReturnsFalse_WhenNotEnoughCoordinates()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1", "a2", "b1",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void AreSquare_ReturnsFalse_WhenNotFourthCoordinateIsWrong()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1",
			"a2",
			"b1",
			"b3",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void AreSquare_ReturnsFalse_WhenVerticalRectangle()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1",
			"a3",
			"b1",
			"b3",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void AreSquare_ReturnsFalse_WhenHorizontalRectangle()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1",
			"a2",
			"c1",
			"c2",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void AreSquare_ReturnsFalse_WhenSquareIsTooBig()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1",
			"a3",
			"c1",
			"c3",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void AreSquare_ReturnsFalse_WhenCoordinatesAreInDiagonalLine()
	{
		// Arrange
		List<Coordinates> coordinatesEnumerable = new()
		{
			"a1",
			"b2",
			"c3",
			"d4",
		};

		// Act
		bool result = SquareChecker.AreSquare(coordinatesEnumerable);

		// Assert
		Assert.False(result);
	}
}