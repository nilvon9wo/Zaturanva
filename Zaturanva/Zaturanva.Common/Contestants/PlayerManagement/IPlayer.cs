using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Contestants.PlayerManagement;

public interface IPlayer
{
	public HashSet<Color> Colors { get; }
	IPlayer Assign(Color color);
	bool IsPlaying(Color color);
	bool IsRegent(Game game, Color color);
	bool OccupiesThrone(Game game, Color color);
}