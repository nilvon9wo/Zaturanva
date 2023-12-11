using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

using Color = Zaturanva.Common.Colors.Color;

namespace Zaturanva.Common.Tests.Pieces;

public class PawnTests
{
	[Theory]
	[InlineData(
		Color.White,
		"a2",
		"a1",
		false
	)]
	[InlineData(
		Color.White,
		"a2",
		"a3",
		true
	)]
	[InlineData(
		Color.White,
		"a2",
		"a4",
		false
	)]
	[InlineData(
		Color.White,
		"a2",
		"b2",
		false
	)]
	[InlineData(
		Color.White,
		"a2",
		"c2",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"a5",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"a6",
		true
	)]
	[InlineData(
		Color.Black,
		"a7",
		"b7",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"c7",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"a2",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"c2",
		true
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"d2",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"b1",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"b3",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"e2",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"f2",
		true
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"g1",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"g3",
		false
	)]
	public void CanMoveOneStepForward(
		Color color,
		string initialPosition,
		string finalPosition,
		bool expected
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(color);
		Pawn pawn = CreatePawnAt(player, initialPosition, color);
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, finalPosition);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(
		Color.White,
		"a2",
		"a4",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"a5",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"d2",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"e2",
		false
	)]
	public void CannotMoveTwoStepsForward(
		Color color,
		string initialPosition,
		string finalPosition,
		bool expected
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(color);
		Pawn pawn = CreatePawnAt(player, initialPosition, color);
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, finalPosition);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(
		Color.White,
		"a2",
		"b3",
		true
	)]
	[InlineData(
		Color.Black,
		"a7",
		"b6",
		true
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"c3",
		true
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"f1",
		true
	)]
	public void CanCaptureDiagonally(
		Color color,
		string initialPosition,
		string finalPosition,
		bool expected
	)
	{
		// Arrange
		IPlayer player1 = new Player().Assign(color);
		IPlayer player2 = new Player().Assign(Color.Blue);
		Pawn pawn = CreatePawnAt(player1, initialPosition, color);
		Pawn enemyPawn = CreatePawnAt(player2, finalPosition, Color.Blue);
		GameState game = CreateGameFor(new List<IPiece> { pawn, enemyPawn });

		// Act
		bool result = pawn.CanMoveTo(game, finalPosition);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(
		Color.White,
		"a2",
		"b3",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"b6",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"c3",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"f1",
		false
	)]
	public void CannotMoveDiagonallyIfNotCapturing(
		Color color,
		string initialPosition,
		string finalPosition,
		bool expected
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(color);
		Pawn pawn = CreatePawnAt(player, initialPosition, color);
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, finalPosition);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(
		Color.White,
		"a2",
		"a1",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"a8",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"a2",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"h2",
		false
	)]
	public void CannotMoveBackwards(
		Color color,
		string initialPosition,
		string finalPosition,
		bool expected
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(color);
		Pawn pawn = CreatePawnAt(player, initialPosition, color);
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, finalPosition);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(
		Color.White,
		"a2",
		"b2",
		false
	)]
	[InlineData(
		Color.Black,
		"a7",
		"b7",
		false
	)]
	[InlineData(
		Color.Blue,
		"b2",
		"b3",
		false
	)]
	[InlineData(
		Color.Orange,
		"g2",
		"g1",
		false
	)]
	public void CannotMoveHorizontally(
		Color color,
		string initialPosition,
		string finalPosition,
		bool expected
	)
	{
		// Arrange
		IPlayer player = new Player().Assign(color);
		Pawn pawn = CreatePawnAt(player, initialPosition, color);
		GameState game = CreateGameFor(pawn);

		// Act
		bool result = pawn.CanMoveTo(game, finalPosition);

		// Assert
		Assert.Equal(expected, result);
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
				AllowActiveColorSelfCapture = true,
				AllowMovingColorSelfCapture = true,
				AllowPlayerSelfCapture = true,
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