using Ardalis.GuardClauses;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Games;

public static class GameStateHandler
{
	public static bool CanMove(GameState game, IPiece piece)
	{
		_ = Guard.Against.Null(game);
		_ = Guard.Against.Null(piece);
		Color currentPlayerColor = Guard.Against.Null(game.ActiveColor);

		return game.TurnPhase switch
		{
			TurnPhase.FirstMove
				=> IsNormalMovement(game, piece),

			TurnPhase.SecondMove
				=> ((game.FocusColor == currentPlayerColor)
					&& IsNormalMovement(game, piece))
				   || ((game.FocusColor != currentPlayerColor)
					   && IsOccupierMovement(game, piece)),

			_
				=> throw new NotImplementedException(
					$"{game.TurnPhase} is not implemented yet."
				),
		};
	}

	private static bool IsNormalMovement(GameState game, IPiece piece)
		=> (game.ActiveColor == piece.Color)
		   || IsOccupierMovement(game, piece)
		   || IsRegent(game, piece.Color)
		   || IsBoatTriumphReward(game, piece);

	private static bool IsBoatTriumphReward(GameState game, IPiece piece)
		=> piece is Boat boat
		   && (boat.SharedWithForBoatTriumph == game.ActiveColor);

	private static bool IsOccupierMovement(GameState game, IPiece piece)
	{
		Color currentPlayerColor = Guard.Against.Null(game.ActiveColor);
		Raja currentPlayerRaja = game[currentPlayerColor].Raja;
		Coordinates pieceThroneLocation
			= game.Board.GetThroneLocation(piece.Color);
		return currentPlayerRaja.Location == pieceThroneLocation;
	}

	private static bool IsRegent(GameState game, Color pieceColor)
	{
		Color currentPlayerColor = Guard.Against.Null(game.ActiveColor);
		Raja currentPlayerRaja = game[currentPlayerColor].Raja;
		bool currentPlayerRajaIsFree = currentPlayerRaja.CapturedBy
									   == LanguageExt.Option<Color>.None;

		Raja targetColorPlayerRaja = game[pieceColor].Raja;
		bool targetColorPlayerRajaIsNotFree = targetColorPlayerRaja.CapturedBy
											  != LanguageExt.Option<Color>.None;

		bool areAllies = AreAllies(game, pieceColor);
		return currentPlayerRajaIsFree
			   && targetColorPlayerRajaIsNotFree
			   && areAllies;
	}

	private static bool AreAllies(GameState game, Color pieceColor)
	{
		Color currentPlayerColor = Guard.Against.Null(game.ActiveColor);
		Alliance currentAlliance = game.FindAllianceFor(currentPlayerColor);
		Alliance pieceAlliance = game.FindAllianceFor(pieceColor);
		return currentAlliance == pieceAlliance;
	}

	public static bool CanMoveTo(
		GameState game,
		IPiece piece,
		Coordinates coordinates
	)
		=> throw new NotImplementedException();

	public static GameState MoveTo(
		GameState game,
		IPiece piece,
		Coordinates coordinates
	)
		=> throw new NotImplementedException();
}