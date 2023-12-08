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

	public static bool IsEnemyOf(this Color thisColor, Color otherColor)
		=> (_achromatics.Contains(thisColor) && _vivids.Contains(otherColor))
		   || (_vivids.Contains(thisColor)
			   && _achromatics.Contains(otherColor));
}