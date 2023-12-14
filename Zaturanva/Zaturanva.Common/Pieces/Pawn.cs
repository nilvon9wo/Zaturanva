using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class Pawn(Color color, IPlayer owner) : IPiece
{
#pragma warning disable CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.
	public Color Color { get; init; } = color;

	public IPlayer Owner { get; init; } = owner;
#pragma warning restore CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.

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

	private bool IsMovementAllowed(
		Board board,
		Coordinates currentLocation,
		Coordinates destination
	)
		=> IsOneStepForward(board, currentLocation, destination)
		   || IsDiagonalCapture(board, currentLocation, destination);

	private bool IsOneStepForward(
		Board board,
		Coordinates currentLocation,
		Coordinates destination
	)
	{
		int xDifference = destination.X - currentLocation.X;
		int yDifference = destination.Y - currentLocation.Y;
		int stepSize = GetStepSize(currentLocation, destination);
		return (stepSize == 1)
			   && ((Color is Color.White or Color.Black
					&& (xDifference == 0))
				   || (Color is Color.Blue or Color.Orange
					   && (yDifference == 0)))
			   && board[destination]
				   .IsVacant();
	}

	private bool IsDiagonalCapture(
		Board board,
		Coordinates currentLocation,
		Coordinates destination
	)
	{
		int xDifference = destination.X - currentLocation.X;
		int yDifference = destination.Y - currentLocation.Y;
		int stepSize = GetStepSize(currentLocation, destination);
		return (Math.Abs(xDifference) == 1)
			   && (Math.Abs(yDifference) == 1)
			   && (stepSize == 1)
			   && board[destination]
				   .IsOccupied();
	}

	private int GetStepSize(
		Coordinates currentLocation,
		Coordinates destination
	)
	{
		int xDifference = destination.X - currentLocation.X;
		int yDifference = destination.Y - currentLocation.Y;
		return Color switch
		{
			Color.White => yDifference,
			Color.Black => -yDifference,
			Color.Blue => xDifference,
			Color.Orange => -xDifference,
			_ => throw new ArgumentException("Invalid color"),
		};
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