using System.Security.Cryptography;

namespace Zaturanva.Common.Extensions;

internal static class EnumerableExtensions
{
	internal static IEnumerable<T> Shuffle<T>(this IEnumerable<T> input)
	{
		List<T> result = input.ToList();
		using RandomNumberGenerator randomGenerator
			= RandomNumberGenerator.Create();
		int resultCount = result.Count;
		while (resultCount > 1)
		{
			resultCount--;
			int swapIndex = GetRandomNumber(resultCount, randomGenerator);
			(result[resultCount], result[swapIndex])
				= (result[swapIndex], result[resultCount]);
		}

		return result;
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