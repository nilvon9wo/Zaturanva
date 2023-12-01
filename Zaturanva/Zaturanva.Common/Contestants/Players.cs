using System.Collections;

using Zaturanva.Common.Colors;

namespace Zaturanva.Common.Contestants;

public class Players : IEnumerable<IPlayer>
{
	private readonly Dictionary<Color, IPlayer> _playersByColor = new();

	public void Add(IPlayer player)
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

	public void AddRange(IEnumerable<IPlayer> players)
	{
		ArgumentNullException.ThrowIfNull(players);

		foreach (IPlayer player in players)
		{
			Add(player);
		}
	}

	public IEnumerator<IPlayer> GetEnumerator()
		=> _playersByColor.Keys.Select(color => _playersByColor[color])
			.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public IPlayer this[Color color]
		=> _playersByColor[color];
}