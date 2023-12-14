namespace Zaturanva.Common.Colors;

internal static class ColorExtensions
{
	private static readonly Dictionary<Color, int> _rotationAnglesByColor
		= new()
		{
			{ Color.White, 0 },
			{ Color.Black, 180 },
			{ Color.Blue, 90 },
			{ Color.Orange, 270 },
		};

	private static readonly HashSet<Color> _achromatics
		= ColorUtility.Achromatics;

	private static readonly HashSet<Color> _vivids = ColorUtility.Vivids;

	private static readonly Dictionary<Color, HashSet<Color>> _alliesByColor
		= new()
		{
			{ Color.White, _achromatics },
			{ Color.Black, _achromatics },
			{ Color.Blue, _vivids },
			{ Color.Orange, _vivids },
		};

	private static readonly Dictionary<Color, HashSet<Color>> _enemiesByColor
		= new()
		{
			{ Color.White, _vivids },
			{ Color.Black, _vivids },
			{ Color.Blue, _achromatics },
			{ Color.Orange, _achromatics },
		};

	public static int GetRotation(this Color color)
		=> _rotationAnglesByColor[color];

	private static HashSet<Color> GetAllies(
		this Color color,
		bool includeColor = false
	)
	{
		HashSet<Color> allies = _alliesByColor[color];
		if (!includeColor)
		{
			_ = allies.Remove(color);
		}

		return allies;
	}

	public static IEnumerable<TColored> SelectAllies<TColored>(
		this IEnumerable<TColored> otherItems,
		TColored thisItem,
		bool includeThisItem = false
	) where TColored : IColored
		=> otherItems.Where(
			item => (includeThisItem || !object.ReferenceEquals(item, thisItem))
					&& thisItem.Color.GetAllies()
						.Contains(item.Color)
		);

	public static IEnumerable<TColored> SelectEnemies<TColored>(
		this IEnumerable<TColored> otherItems,
		TColored thisItem
	) where TColored : IColored
		=> otherItems.Where(
			boat => thisItem.Color.GetEnemies()
				.Contains(boat.Color)
		);

	private static HashSet<Color> GetEnemies(this Color color)
		=> _enemiesByColor[color];

	public static bool IsEnemyOf(this Color thisColor, Color otherColor)
		=> (thisColor.IsAchromatic() && otherColor.IsVivid())
		   || (thisColor.IsVivid() && otherColor.IsAchromatic());

	private static bool IsAchromatic(this Color thisColor)
		=> _achromatics.Contains(thisColor);

	private static bool IsVivid(this Color thisColor)
		=> _vivids.Contains(thisColor);
}