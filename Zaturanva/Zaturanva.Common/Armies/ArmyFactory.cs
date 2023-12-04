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
	public static Army CreateForBlack(Players players)
		=> Create(
				Guard.Against.Null(players)[Color.Black],
				Color.Black,
				new()
				{
					{ "h8", typeof(Boat) },
					{ "g8", typeof(Horse) },
					{ "f8", typeof(Elephant) },
					{ "e8", typeof(Raja) },
					{ "h7", typeof(Pawn) },
					{ "g7", typeof(Pawn) },
					{ "f7", typeof(Pawn) },
					{ "e7", typeof(Pawn) },
				}
			)
			.Match(
				army => army,
				ex => throw ex
			);

	public static Army CreateForWhite(Players players)
		=> Create(
				Guard.Against.Null(players)[Color.White],
				Color.White,
				new()
				{
					{ "a1", typeof(Boat) },
					{ "b1", typeof(Horse) },
					{ "c1", typeof(Elephant) },
					{ "d1", typeof(Raja) },
					{ "a2", typeof(Pawn) },
					{ "b2", typeof(Pawn) },
					{ "c2", typeof(Pawn) },
					{ "d2", typeof(Pawn) },
				}
			)
			.Match(
				army => army,
				ex => throw ex
			);

	public static Army CreateForBlue(Players players)
		=> Create(
				Guard.Against.Null(players)[Color.Blue],
				Color.Blue,
				new()
				{
					{ "a8", typeof(Boat) },
					{ "a7", typeof(Horse) },
					{ "a6", typeof(Elephant) },
					{ "a5", typeof(Raja) },
					{ "b8", typeof(Pawn) },
					{ "b7", typeof(Pawn) },
					{ "b6", typeof(Pawn) },
					{ "b5", typeof(Pawn) },
				}
			)
			.Match(
				army => army,
				ex => throw ex
			);

	public static Army CreateForOrange(Players players)
		=> Create(
				Guard.Against.Null(players)[Color.Orange],
				Color.Orange,
				new()
				{
					{ "h1", typeof(Boat) },
					{ "h2", typeof(Horse) },
					{ "h3", typeof(Elephant) },
					{ "h4", typeof(Raja) },
					{ "g1", typeof(Pawn) },
					{ "g2", typeof(Pawn) },
					{ "g3", typeof(Pawn) },
					{ "g4", typeof(Pawn) },
				}
			)
			.Match(
				army => army,
				ex => throw ex
			);

	private static Try<Army> Create(
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