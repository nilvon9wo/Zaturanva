using System.Collections;

using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants;

public class Players : IEnumerable<Player>
{
	private readonly Dictionary<Color, Player> _playersByColor = new();

	public void Add(Player player)
	{
		ArgumentNullException.ThrowIfNull(player);
		HashSet<Color> colors = player.Colors;
		if (colors.Count < 1)
		{
			throw new ArgumentException(
				"Players must each be assigned at least one color",
				nameof(player)
			);
		}

		foreach (Color color in colors)
		{
			_playersByColor[color] = player;
		}
	}

	public IEnumerator<Player> GetEnumerator()
		=> _playersByColor.Values.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public Player this[Color color]
		=> _playersByColor[color];
}