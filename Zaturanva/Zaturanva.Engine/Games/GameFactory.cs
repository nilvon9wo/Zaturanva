using LanguageExt;

using Zaturanva.Common.Armies;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants;
using Zaturanva.Common.Pieces;

// ReSharper disable MemberCanBePrivate.Global

namespace Zaturanva.Engine.Games;

public static class GameFactory
{
	public static Try<Game> CreateFor(IEnumerable<IPlayer> inputPlayers)
		=> inputPlayers
			.ToPlayers()
			.Map(CreateFor);

	private static Game CreateFor(Players players)
		=> new()
		{
			BlackArmy = ArmyFactory.Create(
					players[Color.Black],
					Color.Black,
					new()
					{
						{ "h8", typeof(Boat) },
						{ "g8", typeof(Horse) },
						{ "f8", typeof(Elephant) },
						{ "e8", typeof(King) },
						{ "h7", typeof(Pawn) },
						{ "g7", typeof(Pawn) },
						{ "f7", typeof(Pawn) },
						{ "e7", typeof(Pawn) },
					}
				)
				.Match(
					army => army,
					ex => throw ex
				),
			WhiteArmy = ArmyFactory.Create(
					players[Color.White],
					Color.White,
					new()
					{
						{ "a1", typeof(Boat) },
						{ "b1", typeof(Horse) },
						{ "c1", typeof(Elephant) },
						{ "d1", typeof(King) },
						{ "a2", typeof(Pawn) },
						{ "b2", typeof(Pawn) },
						{ "c2", typeof(Pawn) },
						{ "d2", typeof(Pawn) },
					}
				)
				.Match(
					army => army,
					ex => throw ex
				),
			BlueArmy = ArmyFactory.Create(
					players[Color.Blue],
					Color.Blue,
					new()
					{
						{ "a8", typeof(Boat) },
						{ "a7", typeof(Horse) },
						{ "a6", typeof(Elephant) },
						{ "a5", typeof(King) },
						{ "b8", typeof(Pawn) },
						{ "b7", typeof(Pawn) },
						{ "b6", typeof(Pawn) },
						{ "b5", typeof(Pawn) },
					}
				)
				.Match(
					army => army,
					ex => throw ex
				),
			OrangeArmy = ArmyFactory.Create(
					players[Color.Orange],
					Color.Orange,
					new()
					{
						{ "h1", typeof(Boat) },
						{ "h2", typeof(Horse) },
						{ "h3", typeof(Elephant) },
						{ "h4", typeof(King) },
						{ "g1", typeof(Pawn) },
						{ "g2", typeof(Pawn) },
						{ "g3", typeof(Pawn) },
						{ "g4", typeof(Pawn) },
					}
				)
				.Match(
					army => army,
					ex => throw ex
				),
		};
}