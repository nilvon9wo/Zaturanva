using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants.PlayerManagement;

namespace Zaturanva.Common.Games;

public class Game
{
	public required Players Players { get; init; }

	public IPlayer? CurrentPlayer { get; set; }

	public required Board Board { get; init; }

	public required Army BlackArmy { get; init; }
	public required Army WhiteArmy { get; init; }
	public required Army BlueArmy { get; init; }
	public required Army OrangeArmy { get; init; }

	public Alliance Achromatic
		=> new() { BlackArmy, WhiteArmy };

	public Alliance Vivids
		=> new() { BlueArmy, OrangeArmy };
}