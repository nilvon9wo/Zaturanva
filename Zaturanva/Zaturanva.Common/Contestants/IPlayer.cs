using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants;

public interface IPlayer
{
	public HashSet<Color> Colors { get; }
	IPlayer Assign(Color color);
	bool IsPlaying(Color color);
}