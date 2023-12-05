using Ardalis.GuardClauses;

using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

internal static class PieceFactory
{
	internal static Try<IPiece> Create(
		IPlayer owner,
		Color color,
		KeyValuePair<Coordinates, Type> coordinateTypePair
	)
		=> Try<IPiece>(
			() =>
			{
				int rotationAngle = color.GetRotation();
				Coordinates coordinates
					= coordinateTypePair.Key.Rotate(rotationAngle);

				Type pieceType = coordinateTypePair.Value;
				object pieceInstance = Activator.CreateInstance(pieceType)
									   ?? throw new InvalidPieceException(
										   $"Can't create {pieceType}."
									   );
				switch (pieceInstance)
				{
					case IPiece piece:
						piece.Location = Option<Coordinates>.Some(coordinates);
						piece.Color = color;
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