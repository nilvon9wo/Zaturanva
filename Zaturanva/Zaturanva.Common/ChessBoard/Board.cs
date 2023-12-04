using Ardalis.GuardClauses;

using LanguageExt;

using System.Diagnostics.CodeAnalysis;

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

	public static Board From(IEnumerable<IPiece> pieces)
		=> new()
		{
			_cellByCoordinates = Guard.Against
				.Null(pieces)
				.Aggregate(
					new Dictionary<Coordinates, Cell>(),
					(dict, piece) =>
					{
						_ = piece.Location.IfSome(
							location => dict[location] = new(
								location,
								Option<IPiece>.Some(piece)
							)
						);
						return dict;
					}
				),
		};
}