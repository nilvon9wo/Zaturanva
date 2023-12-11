using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class Elephant : IPiece
{
	public Color Color { get; set; }

	public required IPlayer Owner { get; set; }

	public Option<Color> CapturedBy { get; set; } = Option<Color>.None;

	public required Option<Coordinates> Location { get; set; }

	public bool CanMoveTo(GameState game, Coordinates destination)
		=> Location.Match(
			currentLocation
				=> IsMovementAllowed(game.Board, currentLocation, destination)
				   && game.IsMoveAllowedByStandardRules(destination),
			() => false
		);

	private static bool IsMovementAllowed(
		Board board,
		Coordinates currentLocation,
		Coordinates destination
	)
	{
		int xDifference = Math.Abs(destination.X - currentLocation.X);
		int yDifference = Math.Abs(destination.Y - currentLocation.Y);

		bool isOrthogonal = IsOrthogonal(xDifference, yDifference);
		IEnumerable<int> range = Enumerable.Range(
			1,
			Math.Max(xDifference, yDifference) - 1
		);
		IEnumerable<Coordinates> inBetweenCells = range
			.Select(
				i => new Coordinates(
					CalculateX(currentLocation, destination, i),
					CalculateY(currentLocation, destination, i)
				)
			);
		bool areAllEmpty = inBetweenCells
			.All(
				cell => board[cell]
					.IsVacant()
			);
		return isOrthogonal
			   && areAllEmpty;
	}

	private static int CalculateX(
		Coordinates currentLocation,
		Coordinates destination,
		int i
	)
	{
		int currentX = currentLocation.X;
		int result = destination.X - currentX;
		return currentX
			   + (Math.Abs(result) > 0
				   ? i * Math.Sign(result)
				   : 0);
	}

	private static int CalculateY(
		Coordinates currentLocation,
		Coordinates destination,
		int i
	)
	{
		int currentY = currentLocation.Y;
		int result = destination.Y - currentY;
		return currentY
			   + (Math.Abs(result) > 0
				   ? i * Math.Sign(result)
				   : 0);
	}

	private static bool IsOrthogonal(int xDifference, int yDifference)
	{
		bool isHorizontal = (xDifference > 0) && (yDifference == 0);
		bool isVertical = (xDifference == 0) && (yDifference > 0);
		return isHorizontal || isVertical;
	}
}