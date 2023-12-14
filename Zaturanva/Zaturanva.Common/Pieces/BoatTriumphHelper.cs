using LanguageExt.UnsafeValueAccess;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.ChessBoard.Geometry;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

internal static class BoatTriumphHelper
{
	internal static GameState CheckAndRewardBoatTriumph(
		this Boat boat,
		GameState game,
		Coordinates destination
	)
	{
		Boat[] allBoats = (Boat[])game.Board
			.GetAll(piece => piece is Boat && piece.Location.IsSome)
			.ToArray();
		return allBoats.Length == 4
			? CheckAndRewardBoatTriumph(
				boat,
				game,
				destination,
				allBoats
			)
			: game;
	}

	private static GameState CheckAndRewardBoatTriumph(
		Boat boat,
		GameState game,
		Coordinates destination,
		Boat[] allBoats
	)
	{
		Dictionary<Color, Coordinates> coordinatesByColor
			= allBoats.ToDictionary(
				x => x.Color,
				x => x.Location.ValueUnsafe()
			);
		coordinatesByColor[boat.Color] = destination;
		return SquareChecker.AreSquare(coordinatesByColor.Values)
			? boat.RewardBoatTriumph(game, allBoats)
			: game;
	}

	private static GameState RewardBoatTriumph(
		this Boat thisBoat,
		GameState game,
		IEnumerable<Boat> allBoats
	)
	{
		Boat[] boatArray = allBoats.ToArray();
		boatArray = ShareAllyBoat(thisBoat, boatArray);
		_ = ImprisonEnemyBoats(thisBoat, game, boatArray);

		return game;
	}

	private static Boat[] ShareAllyBoat(
		Boat thisBoat,
		IEnumerable<Boat> boats
	)
	{
		Color thisColor = thisBoat.Color;
		Boat[] boatsArray = boats.ToArray();
		foreach (Boat boat in boatsArray.SelectAllies(thisBoat))
		{
			boat.SharedWithForBoatTriumph
				= thisColor;
		}

		return boatsArray;
	}

	private static Boat[] ImprisonEnemyBoats(
		Boat thisBoat,
		GameState game,
		IEnumerable<Boat> boats
	)
	{
		Boat[] boatsArray = boats.ToArray();
		foreach (Boat boat in boatsArray.SelectEnemies(thisBoat))
		{
			_ = game.Board.Remove(boat)
				.Bind(_ => boat.MakeImprisoned(game, thisBoat.Color))
				.IfFail(exception => throw exception);
		}

		return boatsArray;
	}
}