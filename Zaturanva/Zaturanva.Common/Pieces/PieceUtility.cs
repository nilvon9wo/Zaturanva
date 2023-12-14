using LanguageExt;
using LanguageExt.UnsafeValueAccess;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

internal static class PieceUtility
{
	public static Try<GameState> StandardMoveTo(
		this IPiece piece,
		GameState game,
		Coordinates destination,
		bool canMove = false
	)
		=> Try(
			() =>
			{
				if (canMove || piece.CanMoveTo(game, destination))
				{
					_ = piece.PlaceAt(destination);
					return game;
				}

				throw new InvalidOperationException(
					$"{piece} cannot move to {destination}."
				);
			}
		);

	public static Try<GameState> StandardMakeImprisoned(
		this IPiece piece,
		GameState game,
		Color captor
	)
		=> Try(
			() =>
			{
				if (piece.CapturedBy.IsSome)
				{
					throw new InvalidOperationException(
						$"{piece} is already captured by {piece.CapturedBy.ValueUnsafe()}"
					);
				}

				piece.CapturedBy = captor;
				return game;
			}
		);
}