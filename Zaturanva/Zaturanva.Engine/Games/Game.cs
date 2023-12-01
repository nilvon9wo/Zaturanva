using Zaturanva.Common.Armies;

namespace Zaturanva.Engine.Games;

public class Game
{
	public Alliance ShadeTeam
		=> new() { BlackArmy!, WhiteArmy! };

	public Alliance ColorTeam
		=> new() { BlueArmy!, OrangeArmy! };

	public required Army BlackArmy { get; init; }
	public required Army WhiteArmy { get; init; }
	public required Army BlueArmy { get; init; }
	public required Army OrangeArmy { get; init; }
}