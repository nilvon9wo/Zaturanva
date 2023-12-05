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
	private static readonly Dictionary<Coordinates, Type>
		_initialPieceTypeByCoordinates = new()
		{
			{ "a1", typeof(Boat) },
			{ "b1", typeof(Horse) },
			{ "c1", typeof(Elephant) },
			{ "d1", typeof(Raja) },
			{ "a2", typeof(Pawn) },
			{ "b2", typeof(Pawn) },
			{ "c2", typeof(Pawn) },
			{ "d2", typeof(Pawn) },
		};

	public static Army CreateFor(Players players, Color color)
		=> Create(
				Guard.Against.Null(players)[color],
				color
			)
			.Match(
				army => army,
				ex => throw ex
			);

	private static Try<Army> Create(IPlayer owner, Color color)
		=> Try(
			(Func<Army>)(() => new()
			{
				Owner = Guard.Against.Null(owner),
				Color = color,
				Pieces = Guard.Against.Null(_initialPieceTypeByCoordinates)
					.Select(kvp => CreateFor(owner, color, kvp))
					.ToHashSet(),
			})
		);

	private static IPiece CreateFor(
		IPlayer owner,
		Color color,
		KeyValuePair<Coordinates, Type> coordinateTypePair
	)
	{
		int rotationAngle = color.GetRotation();
		return PieceFactory.Create(owner, color, coordinateTypePair)
			.Match(
				piece => piece,
				exception => throw exception
			);
	}
}