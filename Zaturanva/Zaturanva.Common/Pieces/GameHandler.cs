using Ardalis.GuardClauses;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class GameHandler
{
	public static bool CanMove(Game game, IPiece piece)
	{
		_ = Guard.Against.Null(game);
		_ = Guard.Against.Null(piece);
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);

		_ = piece.Owner;
		return game.TurnPhase switch
		{
			TurnPhase.FirstMove
				=> IsNormalMovement(game, piece),

			TurnPhase.SecondMove
				=> ((game.CurrentTurnColor == currentPlayerColor)
					&& IsNormalMovement(game, piece))
				   || ((game.CurrentTurnColor != currentPlayerColor)
					   && IsOccupierMovement(game, piece)),

			_
				=> throw new NotImplementedException(
					$"{game.TurnPhase} is not implemented yet."
				),
		};
	}

	private static bool IsNormalMovement(Game game, IPiece piece)
	{
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);
		return (currentPlayerColor == piece.Color)
			   || IsOccupierMovement(game, piece)
			   || IsRegent(game, piece.Color);
	}

	private static bool IsRegent(Game game, Color pieceColor)
	{
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);
		Raja currentPlayerRaja = game[currentPlayerColor].Raja;
		bool currentPlayerRajaIsFree = currentPlayerRaja.CapturedBy
									   == LanguageExt.Option<Color>.None;

		Raja targetColorPlayerRaja = game[pieceColor].Raja;
		bool targetColorPlayerRajaIsNotFree = targetColorPlayerRaja.CapturedBy
											  != LanguageExt.Option<Color>.None;

		const bool areAllies = false; // FIXME
		return currentPlayerRajaIsFree
			   && targetColorPlayerRajaIsNotFree
			   && areAllies;
	}

	private static bool IsOccupierMovement(Game game, IPiece piece)
	{
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);
		Raja currentPlayerRaja = game[currentPlayerColor].Raja;
		Coordinates pieceThroneLocation
			= game.Board.GetThroneLocation(piece.Color);
		return currentPlayerRaja.Location == pieceThroneLocation;
	}

	public bool CanMoveTo(Game game, IPiece piece, Coordinates coordinates)
		=> throw new NotImplementedException();

	public Game MoveTo(Game game, IPiece piece, Coordinates coordinates)
		=> throw new NotImplementedException();
}