using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

internal static class PieceFactory
{
	internal static Try<IPiece> Create(
		IPlayer owner,
		KeyValuePair<Coordinates, Type> kv
	)
		=> Try(
			() =>
			{
				Type pieceType = kv.Value;
				object? pieceInstance = Activator.CreateInstance(pieceType)
										?? throw new InvalidPieceException(
											$"Can't create {pieceType}."
										);
				if (pieceInstance is not IPiece piece)
				{
					throw new InvalidPieceException(
						$"{pieceType} cannot be cast to IPiece."
					);
				}

				piece.Location = Option<Coordinates>.Some(kv.Key);
				piece.Owner = owner;
				return piece;
			}
		);
}