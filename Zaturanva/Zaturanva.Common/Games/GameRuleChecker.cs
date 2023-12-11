using Ardalis.GuardClauses;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Games;

public static class GameRuleChecker
{
	public static bool IsMoveAllowedByStandardRules(
		this GameState game,
		IPiece movingPiece,
		Coordinates destination
	)
		=> Guard.Against.Null(game)
			   .Board[destination]
			   .Match(
				   cell => CheckAgainstStandardRules(game, movingPiece, cell),
				   () => false
			   )
		   && Guard.Against.Null(movingPiece)
			   .Location
			   .Match(
				   location => location != destination,
				   () => false
			   );

	private static bool CheckAgainstStandardRules(
		GameState game,
		IPiece movingPiece,
		Cell cell
	)
		=> cell.Piece.Match(
			targetPiece => IsCaptureAllowed(game, movingPiece, targetPiece),
			() => true
		);

	private static bool IsCaptureAllowed(
		GameState game,
		IPiece movingPiece,
		IPiece targetPiece
	)
	{
		Color movingPieceColor = movingPiece.Color;
		Color activePlayerColor = game.ActiveColor
								  ?? throw
									  new
										  InvalidOperationException(
											  "Active color required to move pieces."
										  );
		Color targetPieceColor = targetPiece.Color;
		return (game.GameOptions.AllowMovingColorSelfCapture
				|| (targetPieceColor != movingPieceColor))
			   && (game.GameOptions.AllowAllyCapture
				   || activePlayerColor.IsEnemyOf(targetPieceColor)
				   || (targetPieceColor == activePlayerColor))
			   && (game.GameOptions.AllowActiveColorSelfCapture
				   || (targetPieceColor != activePlayerColor))
			   && (game.GameOptions.AllowPlayerSelfCapture
				   || ActivePlayerDoesNotOwnTarget(targetPiece, game));
	}

	private static bool ActivePlayerDoesNotOwnTarget(
		IPiece targetPiece,
		GameState game
	)
	{
		Color activePlayerColor = game.ActiveColor
								  ?? throw
									  new
										  InvalidOperationException(
											  "Active color required to move pieces."
										  );
		return game.Players[activePlayerColor] != targetPiece.Owner;
	}
}