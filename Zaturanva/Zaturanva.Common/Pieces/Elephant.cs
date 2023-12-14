using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

public class Elephant(Color color, IPlayer owner) : IPiece
{
	public Color Color { get; init; } = color;

	public IPlayer Owner { get; init; } = owner;

	public Option<Color> CapturedBy { get; set; } = Option<Color>.None;

	public Option<Coordinates> Location { get; private set; }
		= Option<Coordinates>.None;

	public IPiece PlaceAt(Coordinates destination)
	{
		Location = destination;
		return this;
	}

	public bool CanMoveTo(GameState game, Coordinates destination)
		=> Location.Match(
			currentLocation
				=> IsMovementAllowed(game.Board, currentLocation, destination)
				   && game.IsMoveAllowedByStandardRules(this, destination),
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

	public Try<GameState> MoveTo(
		GameState game,
		Coordinates destination,
		bool canMove = false
	)
		=> this.StandardMoveTo(game, destination, canMove);

	public Try<GameState> MakeImprisoned(GameState game, Color captor)
	{
		Try<GameState> imprisonAttempt
			= this.StandardMakeImprisoned(game, color);
		if (imprisonAttempt.IsSucc())
		{
			Location = Option<Coordinates>.None;
		}

		return imprisonAttempt;
	}
}