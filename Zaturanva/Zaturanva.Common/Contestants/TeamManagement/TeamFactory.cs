using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Extensions;

namespace Zaturanva.Common.Contestants.TeamManagement;

internal static class TeamFactory
{
	internal static Dictionary<Team, List<IPlayer>> ToTeams(
		IReadOnlyList<IPlayer> players
	)
	{
		List<IPlayer> distinctPlayers = players.FindDistinct();

		return distinctPlayers.Count switch
		{
			2 => CreateTeamsFor2DistinctPlayers(distinctPlayers),
			3 => CreateTeamsFor3DistinctPlayers(players),
			4 => CreateTeamsFor4DistinctPlayers(distinctPlayers),
			_ => throw new PlayersException(
				"Unexpected number of distinct players."
			),
		};
	}

	private static Dictionary<Team, List<IPlayer>>
		CreateTeamsFor2DistinctPlayers(IReadOnlyList<IPlayer> distinctPlayers)
	{
		List<IPlayer> shuffledPlayers = distinctPlayers.Shuffle()
			.ToList();
		return new()
		{
			[Team.Achromatics]
				= new() { shuffledPlayers[0], shuffledPlayers[0] },
			[Team.Vivids]
				= new() { shuffledPlayers[1], shuffledPlayers[1] },
		};
	}

	private static Dictionary<Team, List<IPlayer>>
		CreateTeamsFor3DistinctPlayers(IReadOnlyList<IPlayer> distinctPlayers)
	{
		IPlayer duplicatedPlayer = distinctPlayers.FindFirstDuplicated();
		Team firstTeam = TeamUtility.GetRandom();
		return new()
		{
			[firstTeam] = new() { duplicatedPlayer! },
			[TeamUtility.GetOther(firstTeam)] = distinctPlayers.Shuffle()
				.ToList()
				.Where(player => player != duplicatedPlayer)
				.ToList(),
		};
	}

	private static Dictionary<Team, List<IPlayer>>
		CreateTeamsFor4DistinctPlayers(IReadOnlyList<IPlayer> distinctPlayers)
	{
		List<IPlayer> shuffledPlayers = distinctPlayers.Shuffle()
			.ToList();
		return new()
		{
			[Team.Achromatics]
				= new() { shuffledPlayers[0], shuffledPlayers[1] },
			[Team.Vivids]
				= new() { shuffledPlayers[2], shuffledPlayers[3] },
		};
	}
}