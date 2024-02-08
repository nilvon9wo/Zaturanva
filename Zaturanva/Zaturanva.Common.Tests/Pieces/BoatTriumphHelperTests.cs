using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Pieces;

public static class BoatTriumphHelperTests
{
	// Create some players for testing
	private static readonly IPlayer _blackPlayer = CreatePlayer(Color.Black);
	private static readonly IPlayer _whitePlayer = CreatePlayer(Color.White);
	private static readonly IPlayer _bluePlayer = CreatePlayer(Color.Blue);
	private static readonly IPlayer _orangePlayer = CreatePlayer(Color.Orange);

	[Fact]
	public static void
		CheckAndRewardBoatTriumph_ReturnsUpdatedGameState_WhenBoatsFormASquare()
	{
		// Arrange
		Boat blackBoat = CreateBoatAt(_blackPlayer, "c3");
		Boat whiteBoat = CreateBoatAt(_whitePlayer, "b2");
		Boat blueBoat = CreateBoatAt(_bluePlayer, "b1");
		Boat orangeBoat = CreateBoatAt(_orangePlayer, "a2");

		GameState game = CreateGameFor(
			new IPiece[]
			{
				blackBoat,
				whiteBoat,
				blueBoat,
				orangeBoat,
			}
		);

		Coordinates destination = "a1";

		// Act
		// Call the check and reward boat triumph method
		GameState actualGameState
			= blackBoat.CheckAndRewardBoatTriumph(
				game,
				destination
			);

		// Assert
		// Verify that the actual game state matches the expected game state
		// FIXME: `CheckAndRewardBoatTriumph` is not a pure function.  All instances of `GameState` are the same instance`.
		// TODO:
		Assert.NotNull(actualGameState);
	}

	[Fact]
	public static void
		CheckAndRewardBoatTriumph_ReturnsSameGameState_WhenBoatsDoNotFormASquare()
	{
		// Arrange
		Boat blackBoat = CreateBoatAt(_blackPlayer, "c3");
		Boat whiteBoat = CreateBoatAt(_whitePlayer, "b2");
		Boat blueBoat = CreateBoatAt(_bluePlayer, "b1");
		Boat orangeBoat = CreateBoatAt(_orangePlayer, "a2");

		GameState game = CreateGameFor(
			new IPiece[]
			{
				blackBoat,
				whiteBoat,
				blueBoat,
				orangeBoat,
			}
		);

		Coordinates destination = "e5";

		// Act
		GameState actualGameState
			= blackBoat.CheckAndRewardBoatTriumph(
				game,
				destination
			);

		// Assert
		// Verify that the actual game state matches the expected game state
		// FIXME: `CheckAndRewardBoatTriumph` is not a pure function.  All instances of `GameState` are the same instance`.
		// TODO:
		Assert.NotNull(actualGameState);
	}

	// Helper methods to create players, boats, and game state
	private static IPlayer CreatePlayer(Color color)
		=> new Player().Assign(color);

	private static Boat CreateBoatAt(IPlayer player, string initialPosition)
		=> (Boat)new Boat(player.Colors.First(), player)
			.PlaceAt(initialPosition);

	private static GameState CreateGameFor(IEnumerable<IPiece> pieces)
	{
		IPiece[] pieceArray = pieces.ToArray();
		IEnumerable<IPlayer> piecePlayers = pieceArray
			.Select(piece => piece.Owner)
			.Distinct();
		Players players = new();
		players.AddRange(piecePlayers);
		return new() { Players = players, Board = Board.From(pieceArray) };
	}
}