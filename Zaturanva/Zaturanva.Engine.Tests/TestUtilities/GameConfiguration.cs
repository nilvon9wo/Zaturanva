using Zaturanva.Common.Armies;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Engine.Tests.TestUtilities;

internal static class GameConfiguration
{
	private static readonly Dictionary<string, Type> _blackArmyConfiguration
		= new()
		{
			{ "h8", typeof(Boat) },
			{ "g8", typeof(Horse) },
			{ "f8", typeof(Elephant) },
			{ "e8", typeof(Raja) },
			{ "h7", typeof(Pawn) },
			{ "g7", typeof(Pawn) },
			{ "f7", typeof(Pawn) },
			{ "e7", typeof(Pawn) },
		};

	public static readonly List<string> BlackArmyPlacements
		= _blackArmyConfiguration.Keys.ToList();

	private static readonly Dictionary<string, Type> _whiteArmyConfiguration
		= new()
		{
			{ "a1", typeof(Boat) },
			{ "b1", typeof(Horse) },
			{ "c1", typeof(Elephant) },
			{ "d1", typeof(Raja) },
			{ "a2", typeof(Pawn) },
			{ "b2", typeof(Pawn) },
			{ "c2", typeof(Pawn) },
			{ "d2", typeof(Pawn) },
		};

	public static readonly List<string> WhiteArmyPlacements
		= _whiteArmyConfiguration.Keys.ToList();

	private static readonly Dictionary<string, Type> _blueArmyConfiguration
		= new()
		{
			{ "a8", typeof(Boat) },
			{ "a7", typeof(Horse) },
			{ "a6", typeof(Elephant) },
			{ "a5", typeof(Raja) },
			{ "b8", typeof(Pawn) },
			{ "b7", typeof(Pawn) },
			{ "b6", typeof(Pawn) },
			{ "b5", typeof(Pawn) },
		};

	public static readonly List<string> BlueArmyPlacements
		= _blueArmyConfiguration.Keys.ToList();

	private static readonly Dictionary<string, Type> _orangeArmyConfiguration
		= new()
		{
			{ "h1", typeof(Boat) },
			{ "h2", typeof(Horse) },
			{ "h3", typeof(Elephant) },
			{ "h4", typeof(Raja) },
			{ "g1", typeof(Pawn) },
			{ "g2", typeof(Pawn) },
			{ "g3", typeof(Pawn) },
			{ "g4", typeof(Pawn) },
		};

	public static readonly List<string> OrangeArmyPlacements
		= _orangeArmyConfiguration.Keys.ToList();

	private static readonly Dictionary<Color, Dictionary<string, Type>>
		_configurationByColor = new()
		{
			{ Color.Black, _blackArmyConfiguration },
			{ Color.White, _whiteArmyConfiguration },
			{ Color.Blue, _blueArmyConfiguration },
			{ Color.Orange, _orangeArmyConfiguration },
		};

	public static Type GetExpectedPieceType(Army army, string placement)
		=> _configurationByColor.TryGetValue(
			army.Color,
			out Dictionary<string, Type>? configuration
		)
			? configuration.TryGetValue(placement, out Type? type)
				? type
				: throw new InvalidDataException(
					$"Unexpected placement at {placement}."
				)
			: throw new InvalidDataException($"Unexpected color {army.Color}.");
}