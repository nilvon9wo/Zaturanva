using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants;

// ReSharper disable once ClassNeverInstantiated.Global
public class Player : IPlayer
{
	public HashSet<Color> Colors { get; } = new();

	public IPlayer Assign(Color color)
	{
		_ = Colors.Add(color);
		return this;
	}

	public bool IsPlaying(Color color)
		=> Colors.Contains(color);
}