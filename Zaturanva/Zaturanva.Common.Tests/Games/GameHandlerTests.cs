using LanguageExt;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Games;

public class GameHandlerTests
{
	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnTrue_ForNormalMovementAndCorrectColor(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		IPiece piece = game[currentColor]
			.Pieces.First(p => p is Pawn);

		bool result = GameHandler.CanMove(game, piece);

		Assert.True(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnFalse_ForNormalMovementAndDifferentColor(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		IPiece piece = game[Color.White]
			.Pieces.First(p => p is Pawn);

		bool result = GameHandler.CanMove(game, piece);

		Assert.False(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnTrue_WhenAllyPieceAndAllyRajaIsCaptured(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color allyColor = Color.White;
		IPiece piece = game[allyColor]
			.Pieces.First(p => p is Pawn);
		game[allyColor].Raja.CapturedBy = Option<Color>.Some(Color.Orange);

		bool result = GameHandler.CanMove(game, piece);

		Assert.True(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void
		CanMove_ShouldReturnFalse_WhenAllyPieceAndAllyRajaIsNotCaptured(
			TurnPhase turnPhase
		)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color allyColor = Color.White;
		IPiece piece = game[allyColor]
			.Pieces.First(p => p is Pawn);
		game[allyColor].Raja.CapturedBy = Option<Color>.None;

		bool result = GameHandler.CanMove(game, piece);

		Assert.False(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnTrue_ForBoatTriumphReward(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color allyColor = Color.White;
		Boat boat = (Boat)game[allyColor]
			.Pieces.First(p => p is Boat);
		boat.SharedWithForBoatTriumph = Option<Color>.Some(Color.Black);

		bool result = GameHandler.CanMove(game, boat);

		Assert.True(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnFalse_ForSomeoneElseBoatTriumphReward(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color otherColor = Color.Blue;
		Boat boat = (Boat)game[otherColor]
			.Pieces.First(p => p is Boat);
		boat.SharedWithForBoatTriumph = Option<Color>.Some(Color.Orange);

		bool result = GameHandler.CanMove(game, boat);

		Assert.False(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnFalse_ForNoBoatTriumphReward(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color allyColor = Color.White;
		Boat boat = (Boat)game[allyColor]
			.Pieces.First(p => p is Boat);
		boat.SharedWithForBoatTriumph = Option<Color>.None;

		bool result = GameHandler.CanMove(game, boat);

		Assert.False(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnTrue_WhenRajaOccupiesThrone(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color enemyColor = Color.Blue;
		game[Color.Black].Raja.Location
			= game.Board.GetThroneLocation(enemyColor);

		IPiece piece = game[enemyColor]
			.Pieces.First(p => p is Pawn);

		bool result = GameHandler.CanMove(game, piece);

		Assert.True(result);
	}

	[Theory]
	[InlineData(TurnPhase.FirstMove)]
	[InlineData(TurnPhase.SecondMove)]
	public void CanMove_ShouldReturnFalse_WhenRajaDoesNotOccupyThrone(
		TurnPhase turnPhase
	)
	{
		GameState game = SetupGameForNormalMovement();
		const Color currentColor = Color.Black;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = currentColor;
		game.TurnPhase = turnPhase;

		const Color enemyColor = Color.Blue;
		IPiece piece = game[enemyColor]
			.Pieces.First(p => p is Pawn);

		bool result = GameHandler.CanMove(game, piece);

		Assert.False(result);
	}

	[Fact]
	public void
		CanMove_ShouldReturnTrue_OccupierPlaysSecondPhaseBecauseRajaOccupiesThrone()
	{
		GameState game = SetupGameForNormalMovement();
		const Color occupierColor = Color.Black;
		const Color currentColor = Color.Orange;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = occupierColor;
		game.TurnPhase = TurnPhase.SecondMove;

		game[Color.Black].Raja.Location
			= game.Board.GetThroneLocation(currentColor);

		IPiece piece = game[currentColor]
			.Pieces.First(p => p is Pawn);

		bool result = GameHandler.CanMove(game, piece);

		Assert.True(result);
	}

	[Theory]
	[InlineData(Color.Black)]
	[InlineData(Color.Blue)]
	[InlineData(Color.White)]
	public void
		CanMove_ShouldReturnFalse_OccupierMovesAnyPieceOtherThanCurrentColorThrone(
			Color pieceColor
		)
	{
		GameState game = SetupGameForNormalMovement();
		const Color occupierColor = Color.Black;
		const Color currentColor = Color.Orange;
		game.CurrentTurnColor = currentColor;
		game.WaitingForColor = occupierColor;
		game.TurnPhase = TurnPhase.SecondMove;

		game[Color.Black].Raja.Location
			= game.Board.GetThroneLocation(currentColor);

		IPiece piece = game[pieceColor]
			.Pieces.First(p => p is Pawn);

		bool result = GameHandler.CanMove(game, piece);

		Assert.False(result);
	}

	private static GameState SetupGameForNormalMovement()
		=> CreateGameFor(CreatePlayers());

	private static Players CreatePlayers()
		=> new()
		{
			new Player().Assign(Color.Black),
			new Player().Assign(Color.White),
			new Player().Assign(Color.Blue),
			new Player().Assign(Color.Orange),
		};

	private static GameState CreateGameFor(Players players)
	{
		Army blackArmy = ArmyFactory.CreateFor(players, Color.Black);
		Army whiteArmy = ArmyFactory.CreateFor(players, Color.White);
		Army blueArmy = ArmyFactory.CreateFor(players, Color.Blue);
		Army orangeArmy = ArmyFactory.CreateFor(players, Color.Orange);
		IEnumerable<IPiece> allPieces = blackArmy.Pieces
			.Concat(whiteArmy.Pieces)
			.Concat(blueArmy.Pieces)
			.Concat(orangeArmy.Pieces);
		return new GameState()
		{
			Players = players,
			Board = Board.From(allPieces),
		}
			.SetBlackArmy(blackArmy)
			.SetBlueArmy(blueArmy)
			.SetOrangeArmy(orangeArmy)
			.SetWhiteArmy(whiteArmy);
	}
}