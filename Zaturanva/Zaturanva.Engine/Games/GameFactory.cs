using LanguageExt;

using Zaturanva.Common.Armies;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;

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
			Players = players,
			BlackArmy = ArmyFactory.CreateFor(players, Color.Black),
			WhiteArmy = ArmyFactory.CreateFor(players, Color.White),
			BlueArmy = ArmyFactory.CreateFor(players, Color.Blue),
			OrangeArmy = ArmyFactory.CreateFor(players, Color.Orange),
		};
}