using Ardalis.GuardClauses;

using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Pieces;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Armies;

public static class ArmyFactory
{
	public static Try<Army> Create(
		IPlayer owner,
		Color color,
		Dictionary<Coordinates, Type> pieceTypeByInitialCoordinates
	)
		=> Try<Army>(
			() => new()
			{
				Owner = Guard.Against.Null(owner),
				Color = color,
				Pieces = Guard.Against.Null(pieceTypeByInitialCoordinates)
					.Select(
						kv => PieceFactory.Create(owner, kv)
							.Match(
								piece => piece,
								exception => throw exception
							)
					)
					.ToHashSet(),
			}
		);
}