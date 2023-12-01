using System.Security.Cryptography;

namespace Zaturanva.Common.Contestants;

internal static class PlayerShuffler
{
	internal static List<T> Shuffle<T>(this IEnumerable<T> inputPlayers)
	{
		List<T> resultPlayers = inputPlayers.ToList();
		using RandomNumberGenerator randomGenerator
			= RandomNumberGenerator.Create();
		int playerCount = resultPlayers.Count;
		while (playerCount > 1)
		{
			playerCount--;
			int swapIndex = GetRandomNumber(playerCount, randomGenerator);
			(resultPlayers[playerCount], resultPlayers[swapIndex])
				= (resultPlayers[swapIndex], resultPlayers[playerCount]);
		}

		return resultPlayers;
	}

	private static int GetRandomNumber(
		int maxValue,
		RandomNumberGenerator randomGenerator
	)
	{
		byte[] randomNumber = new byte[1];
		do
		{
			randomGenerator.GetBytes(randomNumber);
		} while (!IsFairRandomNumber(randomNumber[0], maxValue));

		return randomNumber[0] % maxValue;
	}

	private static bool IsFairRandomNumber(byte randomNumber, int maxValue)
	{
		int fairMax = byte.MaxValue / maxValue * maxValue;
		return randomNumber < fairMax;
	}
}