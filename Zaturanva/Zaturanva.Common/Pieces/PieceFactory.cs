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
		=> Try(
			() =>
			{
				int rotationAngle = color.GetRotation();
				Coordinates coordinates
					= coordinateTypePair.Key.Rotate(rotationAngle);

				Type pieceType = coordinateTypePair.Value;
				object pieceInstance
					= Activator.CreateInstance(pieceType, color, owner)
					  ?? throw new InvalidPieceException(
						  $"Can't create {pieceType}."
					  );
				return pieceInstance switch
				{
					IPiece piece => piece.PlaceAt(coordinates),
					_ => throw new InvalidPieceException(
						$"{pieceType} cannot be cast to IPiece."
					),
				};
			}
		);
}