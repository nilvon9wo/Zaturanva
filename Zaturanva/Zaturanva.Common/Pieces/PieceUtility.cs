using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

internal static class PieceUtility
{
	internal static bool IsMoveAllowedByStandardRules(
		this IPiece movingPiece,
		GameState game,
		Coordinates destination
	)
		=> game.Board[destination]
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
		Color activePlayerColor = game.ActiveColor
								  ?? throw
									  new
										  InvalidOperationException(
											  "Active color required to move pieces."
										  );

		Color targetPieceColor = targetPiece.Color;

		bool isEnemy = movingPieceColor.IsEnemy(targetPieceColor);

		bool activePlayerIsAlly = activePlayerColor.IsAlly(targetPieceColor);
		bool isAllyCaptureAllowed
			= activePlayerIsAlly && game.GameOptions.AllowAllyCapture;

		bool isSameColor = targetPieceColor == movingPieceColor;
		bool isDifferentColor = !isSameColor;
		bool isSameColorCaptureAllowed
			= isSameColor && game.GameOptions.AllowColorSelfCapture;

		IPlayer activePlayer = game.Players[activePlayerColor];
		bool moverDoesNotOwnTarget = activePlayer != targetPiece.Owner;

		return (isEnemy || isAllyCaptureAllowed)
			   && (isDifferentColor || isSameColorCaptureAllowed)
			   && (moverDoesNotOwnTarget
				   || game.GameOptions.AllowPlayerSelfCapture);
	}
}