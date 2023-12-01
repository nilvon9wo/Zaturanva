using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants;

// ReSharper disable once ClassNeverInstantiated.Global
public class Player : IPlayer
{
	public HashSet<Color> Colors { get; } = new();

	public Player Assign(Color color)
	{
		_ = Colors.Add(color);
		return this;
	}
}