using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Pieces;

public class ElephantTests
{
	[Fact]
	public void CanMoveHorizontally()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant = CreateElephantAt(player, "a1");
		GameState game = CreateGameFor(elephant);

		// Act
		bool result = elephant.CanMoveTo(game, "c1");

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CanMoveVertically()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant = CreateElephantAt(player, "a1");
		GameState game = CreateGameFor(elephant);

		// Act
		bool result = elephant.CanMoveTo(game, "a4");

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CannotMoveDiagonally()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant = CreateElephantAt(player, "a1");
		GameState game = CreateGameFor(elephant);

		// Act
		bool result = elephant.CanMoveTo(game, "d4");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CannotMoveToArbitraryPlacesOnBoard()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant = CreateElephantAt(player, "a1");
		GameState game = CreateGameFor(elephant);

		// Act
		bool result = elephant.CanMoveTo(game, "e5");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CannotMoveOffBoard()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant = CreateElephantAt(player, "a1");
		GameState game = CreateGameFor(elephant);

		// Act
		bool result = elephant.CanMoveTo(game, new(-1, 0));

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DoesNotAllowHorizontalMovementWithPiecesInBetween()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant1 = CreateElephantAt(player, "a1");
		Elephant elephant2 = CreateElephantAt(player, "b1");
		GameState game = CreateGameFor(
			new List<IPiece> { elephant1, elephant2 }
		);

		// Act
		bool result = elephant1.CanMoveTo(game, "c1");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DoesNotAllowVerticalMovementWithPiecesInBetween()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant1 = CreateElephantAt(player, "a1");
		Elephant elephant2 = CreateElephantAt(player, "a2");
		GameState game = CreateGameFor(
			new List<IPiece> { elephant1, elephant2 }
		);

		// Act
		bool result = elephant1.CanMoveTo(game, "a3");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DoesNotAllowsHorizontalMovementWithOwnPieceAtDestination()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant1 = CreateElephantAt(player, "a1");
		Elephant elephant2 = CreateElephantAt(player, "c1");
		GameState game = CreateGameFor(
			new List<IPiece> { elephant1, elephant2 }
		);

		// Act
		bool result = elephant1.CanMoveTo(game, "c1");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DoesNotAllowsVerticalMovementWithOwnPieceAtDestination()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Elephant elephant1 = CreateElephantAt(player, "a1");
		Elephant elephant2 = CreateElephantAt(player, "a3");
		GameState game = CreateGameFor(
			new List<IPiece> { elephant1, elephant2 }
		);

		// Act
		bool result = elephant1.CanMoveTo(game, "a3");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void AllowsHorizontalMovementWithEnemyPieceAtDestination()
	{
		// Arrange
		IPlayer whitePlayer = new Player().Assign(Color.White);
		IPlayer bluePlayer = new Player().Assign(Color.Blue);
		Elephant elephant1 = CreateElephantAt(whitePlayer, "a1");
		Elephant elephant2 = CreateElephantAt(bluePlayer, "c1", Color.Blue);
		GameState game = CreateGameFor(
			new List<IPiece> { elephant1, elephant2 }
		);

		// Act
		bool result = elephant1.CanMoveTo(game, "c1");

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void AllowsVerticalMovementWithEnemyPieceAtDestination()
	{
		// Arrange
		IPlayer whitePlayer = new Player().Assign(Color.White);
		IPlayer bluePlayer = new Player().Assign(Color.Blue);
		Elephant elephant1 = CreateElephantAt(whitePlayer, "a1");
		Elephant elephant2 = CreateElephantAt(bluePlayer, "c1", Color.Blue);
		GameState game = CreateGameFor(
			new List<IPiece> { elephant1, elephant2 }
		);

		// Act
		bool result = elephant1.CanMoveTo(game, "a3");

		// Assert
		Assert.True(result);
	}

	private static Elephant CreateElephantAt(
		IPlayer player,
		string initialPosition,
		Color color = Color.White
	)
		=> new()
		{
			Owner = player,
			Location = Option<Coordinates>.Some(initialPosition),
			Color = color,
		};

	private static GameState CreateGameFor(
		Players players,
		IEnumerable<IPiece> allPieces
	)
		=> new()
		{
			Players = players,
			ActiveColor = Color.White,
			Board = Board.From(allPieces),
			GameOptions = new()
			{
				AllowActiveColorSelfCapture = false,
				AllowPlayerSelfCapture = false,
			},
		};

	private static GameState CreateGameFor(IEnumerable<IPiece> allPieces)
	{
		Players players = new();
		IPiece[] pieces = allPieces.ToArray();
		foreach (IPiece piece in pieces)
		{
			players.Add(piece.Owner);
		}

		return CreateGameFor(players, pieces);
	}

	private static GameState CreateGameFor(IPiece boat)
	{
		Players players = new() { boat.Owner };
		IEnumerable<IPiece> allPieces = new List<IPiece> { boat };
		return CreateGameFor(players, allPieces);
	}
}