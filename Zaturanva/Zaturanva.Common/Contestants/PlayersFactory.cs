using LanguageExt;

using Zaturanva.Common.Colors;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Contestants;

public static class PlayersFactory
{
	public static Try<Players> ToPlayers(this IEnumerable<IPlayer> players)
		=> Try(
			() =>
			{
				IPlayer[] playerArray = players.ToArray();
				if (playerArray.Length != 4)
				{
					throw new ArgumentException("Four players are required.");
				}

				List<IPlayer> distinctPlayers = GetDistinctPlayers(playerArray);
				return ShuffleAndAssignColors(distinctPlayers);
			}
		);

	private static List<IPlayer> GetDistinctPlayers(
		IEnumerable<IPlayer> playerArray
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

	private static Players ShuffleAndAssignColors(
		IReadOnlyCollection<IPlayer> distinctPlayers
	)
	{
		List<IPlayer> shuffledPlayers = distinctPlayers.Shuffle();
		return ColorUtility.AllColors.Aggregate(
			new Players(),
			(result, color) =>
			{
				int playerIndex = result.Count() % shuffledPlayers.Count;
				result.Add(
					shuffledPlayers[playerIndex]
						.Assign(color)
				);
				return result;
			}
		);
	}
}