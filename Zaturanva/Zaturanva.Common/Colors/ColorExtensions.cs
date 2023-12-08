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

	public static int GetRotation(this Color color)
		=> _rotationAnglesByColor[color];

	// ReSharper disable once MemberCanBePrivate.Global
	public static bool IsAlly(this Color thisColor, Color otherColor)
		=> (thisColor != otherColor)
		   && ((_achromatics.Contains(thisColor)
				&& _achromatics.Contains(otherColor))
			   || (_vivids.Contains(thisColor)
				   && _vivids.Contains(otherColor)));

	public static bool IsEnemy(this Color thisColor, Color otherColor)
		=> (thisColor != otherColor) && !thisColor.IsAlly(otherColor);
}