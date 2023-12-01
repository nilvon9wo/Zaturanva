using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Extensions;

namespace Zaturanva.Common.Contestants.TeamManagement;

internal static class TeamFactory
{
	internal static Dictionary<Team, List<IPlayer>> ToTeams(
		IReadOnlyList<IPlayer> fourPlayers
	)
	{
		if (fourPlayers.Count != 4)
		{
			throw new ArgumentException("Four players are required.");
		}

		List<IPlayer> distinctPlayers = fourPlayers.FindDistinct();

		return distinctPlayers.Count switch
		{
			2 => CreateTeamsFor2DistinctPlayers(distinctPlayers),
			3 => CreateTeamsFor3DistinctPlayers(fourPlayers),
			4 => CreateTeamsFor4DistinctPlayers(distinctPlayers),
			_ => throw new TeamException(
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
		CreateTeamsFor3DistinctPlayers(IReadOnlyList<IPlayer> allPlayers)
	{
		IPlayer duplicatedPlayer = allPlayers.FindFirstDuplicated();
		Team firstTeam = TeamUtility.GetRandom();
		return new()
		{
			[firstTeam] = new() { duplicatedPlayer },
			[TeamUtility.GetOther(firstTeam)] = allPlayers.Shuffle()
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