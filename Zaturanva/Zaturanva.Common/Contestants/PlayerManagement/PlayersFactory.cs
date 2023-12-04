using LanguageExt;

using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.TeamManagement;
using Zaturanva.Common.Extensions;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Contestants.PlayerManagement;

public static class PlayersFactory
{
	public static Try<Players> ToPlayers(this IEnumerable<IPlayer> players)
		=> Try(
			() =>
			{
				IPlayer[] playerArray = players.ToArray();

				Dictionary<Team, List<IPlayer>> teams
					= TeamFactory.ToTeams(playerArray);
				return ShuffleAndAssignColors(teams);
			}
		);

	private static Players ShuffleAndAssignColors(
		Dictionary<Team, List<IPlayer>> teamsByKey
	)
		=> teamsByKey.Keys
			.SelectMany(
				teamKey => ShufflePlayersAndAssignColors(teamsByKey, teamKey)
			)
			.Aggregate(
				new Players(),
				(allPlayers, teamPlayers) =>
				{
					allPlayers.Add(teamPlayers);
					return allPlayers;
				}
			);

	private static Players ShufflePlayersAndAssignColors(
		IReadOnlyDictionary<Team, List<IPlayer>> teamsByKey,
		Team teamKey
	)
	{
		IEnumerable<Color> teamColors
			= teamKey.GetColors();
		IPlayer[] shuffledPlayers = teamsByKey[teamKey]
			.Shuffle()
			.ToArray();

		return teamColors.Aggregate(
			new Players(),
			(result, color) => AssignColorAndAddPlayer(
				result,
				color,
				shuffledPlayers
			)
		);
	}

	private static Players AssignColorAndAddPlayer(
		Players result,
		Color color,
		IReadOnlyList<IPlayer> shuffledPlayers
	)
	{
		int playerIndex
			= result.Count() % shuffledPlayers.Count;
		result.Add(
			shuffledPlayers[playerIndex]
				.Assign(color)
		);
		return result;
	}
}