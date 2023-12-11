using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Pieces;

public class PawnTests
{
	[Fact]
	public void CanMoveOneStepForward()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Pawn pawn = CreatePawnAt(player, "a2");
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, "a3");

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CannotMoveTwoStepsForward()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Pawn pawn = CreatePawnAt(player, "a2");
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, "a4");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CanCaptureDiagonally()
	{
		// Arrange
		IPlayer player1 = new Player().Assign(Color.White);
		IPlayer player2 = new Player().Assign(Color.Blue);
		Pawn pawn = CreatePawnAt(player1, "a2");
		Pawn enemyPawn = CreatePawnAt(player2, "b3", Color.Blue);
		GameState game = CreateGameFor(new List<IPiece> { pawn, enemyPawn });

		// Act
		bool result = pawn.CanMoveTo(game, "b3");

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void CannotMoveDiagonallyIfNotCapturing()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Pawn pawn = CreatePawnAt(player, "a2");
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, "b3");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CannotMoveBackwards()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Pawn pawn = CreatePawnAt(player, "a2");
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, "a1");

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void CannotMoveHorizontally()
	{
		// Arrange
		IPlayer player = new Player().Assign(Color.White);
		Pawn pawn = CreatePawnAt(player, "a2");
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, "b2");

		// Assert
		Assert.False(result);
	}

	private static Pawn CreatePawnAt(
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