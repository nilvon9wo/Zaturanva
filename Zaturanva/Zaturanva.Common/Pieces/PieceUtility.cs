using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

internal static class PieceUtility
{
	internal static bool ConformsWithStandardDestinationRules(
		this IPiece movingPiece,
		GameState game,
		Coordinates destination
	)
		=> game.Board[destination]
			.Match(
				cell => cell.Piece.Match(
					targetPiece =>
					{
						Color movingPieceColor = movingPiece.Color;
						Color activePlayerColor = game.ActiveColor
												  ?? throw
													  new
														  InvalidOperationException(
															  "Active color required to move pieces."
														  );
						IPlayer playerMovingPiece
							= game.Players[activePlayerColor];
						Color targetPieceColor = targetPiece.Color;
						return (targetPieceColor != movingPieceColor)
							   || ((targetPieceColor == movingPieceColor)
								   && game.GameOptions.AllowColorSelfCapture)
							   || movingPieceColor.IsEnemy(targetPieceColor)
							   || (movingPieceColor.IsAlly(targetPieceColor)
								   && game.GameOptions.AllowAllyCapture)
							   || (playerMovingPiece != targetPiece.Owner)
							   || ((playerMovingPiece == targetPiece.Owner)
								   && game.GameOptions.AllowPlayerSelfCapture);
					},
					() => true
				),
				() => false
			);
}