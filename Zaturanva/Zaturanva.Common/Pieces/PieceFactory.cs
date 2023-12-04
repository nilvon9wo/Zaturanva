using Ardalis.GuardClauses;

using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants.PlayerManagement;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

internal static class PieceFactory
{
	internal static Try<IPiece> Create(
		IPlayer owner,
		KeyValuePair<Coordinates, Type> coordinatesTypePair
	)
		=> Create(owner, coordinatesTypePair.Value, coordinatesTypePair.Key);

	internal static Try<IPiece> Create(
		IPlayer owner,
		Type pieceType,
		Coordinates coordinates
	)
		=> Try(
			() =>
			{
				object pieceInstance = Activator.CreateInstance(pieceType)
									   ?? throw new InvalidPieceException(
										   $"Can't create {pieceType}."
									   );
				switch (pieceInstance)
				{
					case IPiece piece:
						piece.Location = Option<Coordinates>.Some(coordinates);
						piece.Owner = Guard.Against.Null(owner);
						return piece;
					default:
						throw new InvalidPieceException(
							$"{pieceType} cannot be cast to IPiece."
						);
				}
			}
		);
}