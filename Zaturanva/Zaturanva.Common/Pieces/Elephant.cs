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

		if (IsOrthogonal(xDifference, yDifference))
		{
			IEnumerable<Coordinates> intermediateCells = Enumerable.Range(
					1,
					Math.Max(xDifference, yDifference) - 1
				)
				.Select(
					i => new Coordinates(
						currentLocation.X
						+ (xDifference > 0
							? i * Math.Sign(destination.X - currentLocation.X)
							: 0),
						currentLocation.Y
						+ (yDifference > 0
							? i * Math.Sign(destination.Y - currentLocation.Y)
							: 0)
					)
				);

			return intermediateCells.All(cell => board[cell].IsNone);
		}

		return false;
	}

	private static bool IsOrthogonal(int xDifference, int yDifference)
	{
		bool isHorizontal = (xDifference > 0) && (yDifference == 0);
		bool isVertical = (xDifference == 0) && (yDifference > 0);
		return isHorizontal || isVertical;
	}
}