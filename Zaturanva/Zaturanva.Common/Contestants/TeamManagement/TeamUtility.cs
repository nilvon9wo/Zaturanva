using System.Security.Cryptography;

using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants.TeamManagement;

internal static class TeamUtility
{
	internal static Team DetermineTeam(Color color)
		=> ColorUtility.Achromatics.Contains(color)
			? Team.Achromatics
			: Team.Vivids;

	internal static Team GetRandom()
	{
		using RandomNumberGenerator randomGenerator
			= RandomNumberGenerator.Create();
		byte[] randomBytes = new byte[1];

		do
		{
			randomGenerator.GetBytes(randomBytes);
		} while (randomBytes[0] >= 2);

		return randomBytes[0] == 0
			? Team.Achromatics
			: Team.Vivids;
	}

	internal static Team GetOther(Team team)
		=> team == Team.Achromatics
			? Team.Vivids
			: Team.Achromatics;
}