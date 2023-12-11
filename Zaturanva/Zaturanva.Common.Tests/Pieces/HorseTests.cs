using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Pieces;

public class HorseTests
{
	[Theory]
	[InlineData("A1", "C2", true)] // Valid move to the top-right
	[InlineData("D4", "B3", true)] // Valid move to the top-right
	[InlineData("E4", "C3", true)] // Valid move to the bottom-left
	[InlineData("D5", "F4", true)] // Valid move to the top-left
	[InlineData("G7", "E6", true)] // Valid move to the bottom-right
	[InlineData("B2", "A1", false)] // Too close in the top-left direction
	[InlineData("H8", "G5", false)] // Too close in the bottom-right direction
	[InlineData("G7", "D4", false)] // Too far bottom-right direction
	[InlineData("F4", "D4", false)] // Too close horizontally
	[InlineData("C3", "C5", false)] // Too close vertically
	[InlineData("A1", "A3", false)] // Horizontal
	[InlineData("E5", "E7", false)] // Horizontal
	[InlineData("E5", "C5", false)] // Vertical
	[InlineData("G7", "I5", false)] // Move off the board to the bottom-right
	[InlineData("G7", "I9", false)] // Move off the board to the bottom-left
	public void CanMoveTo_ValidAndInvalidMoves_ReturnsExpectedResult(
		string initialPosition,
		string destinationPosition,
		bool expectedResult
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Horse horse = CreateHorseAt(player, initialPosition);
		GameState game = CreateGameFor(horse);

		// Act
		bool canMove = horse.CanMoveTo(game, destinationPosition);

		// Assert
		Assert.Equal(expectedResult, canMove);
	}

	[Theory]
	[InlineData("A1", -2, 2)]
	[InlineData("A1", -2, -2)]
	[InlineData("A1", 2, -2)]
	[InlineData("A2", -2, 3)]
	[InlineData("A2", -2, -1)]
	[InlineData("A2", 2, -1)]
	[InlineData("A3", -2, 4)]
	[InlineData("A3", -2, 0)]
	[InlineData("B1", -1, 2)]
	[InlineData("B1", -1, -2)]
	[InlineData("B1", 3, -2)]
	[InlineData("B2", -1, 3)]
	[InlineData("B2", -1, -1)]
	[InlineData("B2", 3, -1)]
	[InlineData("B3", -1, 4)]
	[InlineData("B3", -1, 0)]
	[InlineData("H1", 5, -2)]
	[InlineData("H1", 9, -2)]
	[InlineData("H2", 5, -1)]
	[InlineData("H2", 9, -1)]
	[InlineData("A8", -2, 9)]
	[InlineData("A8", -2, 5)]
	[InlineData("B8", -1, 9)]
	[InlineData("B8", -1, 5)]
	public void CanMoveTo_ValidAndInvalidMoves_ReturnsExpectedFailure(
		string initialPosition,
		int destinationX,
		int destinationY
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Horse horse = CreateHorseAt(player, initialPosition);
		GameState game = CreateGameFor(horse);

		// Act
		bool canMove = horse.CanMoveTo(game, new(destinationX, destinationY));

		// Assert
		Assert.False(canMove);
	}

	private static Horse CreateHorseAt(IPlayer player, string initialPosition)
		=> new()
		{
			Owner = player,
			Location = Option<Coordinates>.Some(initialPosition),
			Color = Color.White,
		};

	private static GameState CreateGameFor(IPiece horse)
	{
		Players players = new() { horse.Owner };
		IEnumerable<IPiece> allPieces = new List<IPiece> { horse };
		return new() { Players = players, Board = Board.From(allPieces) };
	}
}