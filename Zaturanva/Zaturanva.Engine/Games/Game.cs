using Zaturanva.Common.Armies;
using Zaturanva.Common.Contestants.PlayerManagement;

namespace Zaturanva.Engine.Games;

public class Game
{
	public required Players Players { get; init; }

	public Alliance Achromatics
		=> new() { BlackArmy, WhiteArmy };

	public Alliance Vivids
		=> new() { BlueArmy, OrangeArmy };

	public required Army BlackArmy { get; init; }
	public required Army WhiteArmy { get; init; }
	public required Army BlueArmy { get; init; }
	public required Army OrangeArmy { get; init; }
}