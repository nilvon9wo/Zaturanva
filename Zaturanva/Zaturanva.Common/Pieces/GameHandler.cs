using Ardalis.GuardClauses;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class GameHandler
{
	public static bool CanMove(Game game, IPiece piece)
	{
		_ = Guard.Against.Null(game);
		_ = Guard.Against.Null(piece);
		IPlayer currentPlayer = Guard.Against.Null(game.WaitingForPlayer);

		_ = piece.Owner;
		return game.TurnPhase switch
		{
			TurnPhase.FirstMove
				=> IsNormalMovement(game, piece),

			TurnPhase.SecondMove
				=> ((game.CurrentTurnPlayer == currentPlayer)
					&& IsNormalMovement(game, piece))
				   || ((game.CurrentTurnPlayer != currentPlayer)
					   && IsOccupierMovement(game, piece)),

			_
				=> throw new NotImplementedException(
					$"{game.TurnPhase} is not implemented yet."
				),
		};
	}

	private static bool IsNormalMovement(Game game, IPiece piece)
	{
		IPlayer currentPlayer = Guard.Against.Null(game.WaitingForPlayer);
		return (currentPlayer == piece.Owner)
			   || IsOccupierMovement(game, piece)
			   || currentPlayer.IsRegent(game, piece.Color);
	}

	private static bool IsOccupierMovement(Game game, IPiece piece)
		=> game.WaitingForPlayer!
			.OccupiesThrone(game, piece.Color);

	public bool CanMoveTo(Game game, IPiece piece, Coordinates coordinates)
		=> throw new NotImplementedException();

	public Game MoveTo(Game game, IPiece piece, Coordinates coordinates)
		=> throw new NotImplementedException();
}