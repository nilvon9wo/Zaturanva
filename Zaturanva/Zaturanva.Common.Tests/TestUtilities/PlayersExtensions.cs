using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;

namespace Zaturanva.Common.Tests.TestUtilities;

public static class PlayersExtensions
{
	public static int CountUniqueColors(this Players players)
	{
		ArgumentNullException.ThrowIfNull(players);

		return players.Aggregate(
				new HashSet<Color>(),
				(uniqueColors, player) =>
				{
					uniqueColors.UnionWith(player.Colors);
					return uniqueColors;
				}
			)
			.Count;
	}

	public static bool HasPlayerWithColors(
		this Players players,
		HashSet<Color> targetColors
	)
		=> (
				from IPlayer player in players
				where player.Colors.SetEquals(targetColors)
				select new { }
			)
			.Any();
}