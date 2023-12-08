using Ardalis.GuardClauses;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public static class PieceUtility
{
	public static bool IsMoveAllowedByStandardRules(
		this IPiece movingPiece,
		GameState game,
		Coordinates destination
	)
		=> Guard.Against.Null(game)
			.Board[destination]
			.Match(
				cell => CheckAgainstStandardRules(movingPiece, game, cell),
				() => false
			);

	private static bool CheckAgainstStandardRules(
		IPiece movingPiece,
		GameState game,
		Cell cell
	)
		=> cell.Piece.Match(
			targetPiece => IsCaptureAllowed(movingPiece, game, targetPiece),
			() => true
		);

	private static bool IsCaptureAllowed(
		IPiece movingPiece,
		GameState game,
		IPiece targetPiece
	)
	{
		Color movingPieceColor = movingPiece.Color;
		Color targetPieceColor = targetPiece.Color;
		return (game.GameOptions.AllowAllyCapture
				|| movingPieceColor.IsEnemyOf(targetPieceColor))
			   && (game.GameOptions.AllowColorSelfCapture
				   || (targetPieceColor != movingPieceColor))
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