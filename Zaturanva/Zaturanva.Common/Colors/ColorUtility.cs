namespace Zaturanva.Common.Colors;

public static class ColorUtility
{
	public static readonly Color[] AllColors = Enum.GetValues(typeof(Color))
		.Cast<Color>()
		.ToArray();

	public static readonly HashSet<Color> Achromatics = new()
	{
		Color.Black, Color.White,
	};

	public static readonly HashSet<Color> Vivids = new()
	{
		Color.Blue, Color.Orange,
	};
}