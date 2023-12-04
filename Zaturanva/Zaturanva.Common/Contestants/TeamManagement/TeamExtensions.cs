using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants.TeamManagement;

internal static class TeamExtensions
{
	internal static IEnumerable<Color> GetColors(this Team team)
		=> team switch
		{
			Team.Achromatics => ColorUtility.Achromatics,
			Team.Vivids => ColorUtility.Vivids,
			_ => throw new TeamException("Unknown Team."),
		};
}