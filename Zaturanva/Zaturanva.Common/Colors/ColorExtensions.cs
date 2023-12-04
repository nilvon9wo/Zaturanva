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

	public static int GetRotation(this Color color)
		=> _rotationAnglesByColor[color];
}