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
		=> Try(
			() =>
			{
				Type pieceType = Guard.Against.Null(coordinatesTypePair)
					.Value;
				object pieceInstance = Activator.CreateInstance(pieceType)
									   ?? throw new InvalidPieceException(
										   $"Can't create {pieceType}."
									   );
				switch (pieceInstance)
				{
					case IPiece piece:
						piece.Location
							= Option<Coordinates>.Some(coordinatesTypePair.Key);
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