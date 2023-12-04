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

	[SuppressMessage(
		"CodeQuality",
		"IDE0052:Remove unread private members",
		Justification = "<Pending>"
	)]
	private Dictionary<Coordinates, Color>? _thronesByCoordinates;

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
			_thronesByCoordinates = piecesArray
				.Where(piece => piece is Raja)
				.Aggregate(
					new Dictionary<Coordinates, Color>(),
					(throneDictionary, piece) =>
					{
						_ = piece.Location.IfSome(
							location => throneDictionary[location] = piece.Color
						);
						return throneDictionary;
					}
				),
		};
	}
}