namespace Zaturanva.Common.Contestants.PlayerManagement;

internal static class PlayerFinder
{
	internal static List<IPlayer> FindDistinct(
		this IEnumerable<IPlayer> playerArray
	)
	{
		List<IPlayer> distinctPlayers = playerArray.Distinct()
			.ToList();
		return distinctPlayers.Count < 2
			? throw new ArgumentException(
				"At least 2 distinct players are required."
			)
			: distinctPlayers;
	}

	private static IEnumerable<IPlayer> FindDuplicated(
		this IEnumerable<IPlayer> players
	)
		=> players
			.GroupBy(player => player)
			.Where(group => group.Count() > 1)
			.Select(group => group.Key);

	internal static IPlayer FindFirstDuplicated(
		this IEnumerable<IPlayer> players
	)
		=> players
			.FindDuplicated()
			.First();
}