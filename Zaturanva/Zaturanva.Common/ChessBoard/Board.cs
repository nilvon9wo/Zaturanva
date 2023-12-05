using Ardalis.GuardClauses;

using LanguageExt;

using System.Diagnostics.CodeAnalysis;

using Zaturanva.Common.Colors;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.ChessBoard;

public record Board
{
	private Board()
	{
	}

	[SuppressMessage(
		"CodeQuality",
		"IDE0052:Remove unread private members",
		Justification = "<Pending>"
	)]
	private Dictionary<Coordinates, Cell>? _cellByCoordinates;

	private Dictionary<Color, Coordinates>? _thronesByColor;

	public static Board From(IEnumerable<IPiece> pieces)
	{
		IPiece[] piecesArray = Guard.Against.Null(pieces)
			.ToArray();
		return new()
		{
			_cellByCoordinates = piecesArray
				.Aggregate(
					new Dictionary<Coordinates, Cell>(),
					(coordinateDictionary, piece) =>
					{
						_ = piece.Location.IfSome(
							location => coordinateDictionary[location] = new(
								location,
								Option<IPiece>.Some(piece)
							)
						);
						return coordinateDictionary;
					}
				),
			_thronesByColor = piecesArray
				.Where(piece => piece is Raja)
				.Aggregate(
					new Dictionary<Color, Coordinates>(),
					(throneDictionary, piece) =>
					{
						_ = piece.Location.IfSome(
							location => throneDictionary[piece.Color] = location
						);
						return throneDictionary;
					}
				),
		};
	}

	public Coordinates GetThroneLocation(Color pieceColor)
		=> _thronesByColor![pieceColor];
}