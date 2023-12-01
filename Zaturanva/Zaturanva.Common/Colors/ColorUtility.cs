namespace Zaturanva.Common.Colors;

internal static class ColorUtility
{
	public static readonly Color[] AllColors = Enum.GetValues(typeof(Color))
		.Cast<Color>()
		.ToArray();
}