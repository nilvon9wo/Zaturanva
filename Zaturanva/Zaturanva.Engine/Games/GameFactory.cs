using LanguageExt;

using Zaturanva.Common.Armies;
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
			BlackArmy = ArmyFactory.CreateForBlack(players),
			WhiteArmy = ArmyFactory.CreateForWhite(players),
			BlueArmy = ArmyFactory.CreateForBlue(players),
			OrangeArmy = ArmyFactory.CreateForOrange(players),
		};
}