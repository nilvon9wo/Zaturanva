using Ardalis.GuardClauses;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Games;

public class GameHandler
{
	public static bool CanMove(GameState game, IPiece piece)
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

	private static bool IsNormalMovement(GameState game, IPiece piece)
		=> (game.WaitingForColor == piece.Color)
		   || IsOccupierMovement(game, piece)
		   || IsRegent(game, piece.Color)
		   || IsBoatTriumphReward(game, piece);

	private static bool IsBoatTriumphReward(GameState game, IPiece piece)
		=> piece is Boat boat
		   && (boat.SharedWithForBoatTriumph == game.WaitingForColor);

	private static bool IsOccupierMovement(GameState game, IPiece piece)
	{
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);
		Raja currentPlayerRaja = game[currentPlayerColor].Raja;
		Coordinates pieceThroneLocation
			= game.Board.GetThroneLocation(piece.Color);
		return currentPlayerRaja.Location == pieceThroneLocation;
	}

	private static bool IsRegent(GameState game, Color pieceColor)
	{
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);
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
		Color currentPlayerColor = Guard.Against.Null(game.WaitingForColor);
		Alliance currentAlliance = game.FindAllianceFor(currentPlayerColor);
		Alliance pieceAlliance = game.FindAllianceFor(pieceColor);
		return currentAlliance == pieceAlliance;
	}

	public bool CanMoveTo(GameState game, IPiece piece, Coordinates coordinates)
		=> throw new NotImplementedException();

	public GameState MoveTo(
		GameState game,
		IPiece piece,
		Coordinates coordinates
	)
		=> throw new NotImplementedException();
}