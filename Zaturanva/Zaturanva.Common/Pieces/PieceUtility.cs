using Ardalis.GuardClauses;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public static class PieceUtility
{
	public static bool IsMoveAllowedByStandardRules(
		this GameState game,
		Coordinates destination
	)
		=> Guard.Against.Null(game)
			.Board[destination]
			.Match(
				cell => CheckAgainstStandardRules(game, cell),
				() => false
			);

	private static bool CheckAgainstStandardRules(
		GameState game,
		Cell cell
	)
		=> cell.Piece.Match(
			targetPiece => IsCaptureAllowed(game, targetPiece),
			() => true
		);

	private static bool IsCaptureAllowed(
		GameState game,
		IPiece targetPiece
	)
	{
		Color activePlayerColor = game.ActiveColor
								  ?? throw
									  new
										  InvalidOperationException(
											  "Active color required to move pieces."
										  );
		Color targetPieceColor = targetPiece.Color;
		return (game.GameOptions.AllowAllyCapture
				|| activePlayerColor.IsEnemyOf(targetPieceColor))
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