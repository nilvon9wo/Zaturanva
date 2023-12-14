using Ardalis.GuardClauses;

using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

public class Boat(Color color, IPlayer owner) : IPiece
{
#pragma warning disable CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.
	public Color Color { get; init; } = color;

	public IPlayer Owner { get; init; } = owner;
#pragma warning restore CS9124 // Parameter is captured into the state of the enclosing type and its value is also used to initialize a field, property, or event.

	public Option<Color> CapturedBy { get; set; } = Option<Color>.None;

	public Option<Coordinates> Location { get; private set; }
		= Option<Coordinates>.None;

	public Option<Color> SharedWithForBoatTriumph { get; set; }
		= Option<Color>.None;

	public IPiece PlaceAt(Coordinates destination)
	{
		Location = destination;
		return this;
	}

	public bool CanMoveTo(GameState game, Coordinates destination)
		=> Location.Match(
			currentLocation
				=> IsMovementAllowed(currentLocation, destination)
				   && game.IsMoveAllowedByStandardRules(this, destination),
			() => false
		);

	private static bool IsMovementAllowed(
		Coordinates currentLocation,
		Coordinates destination
	)
	{
		int xDifference = Math.Abs(destination.X - currentLocation.X);
		int yDifference = Math.Abs(destination.Y - currentLocation.Y);
		return (xDifference == 2)
			   && (yDifference == 2);
	}

	public Try<GameState> MoveTo(
		GameState game,
		Coordinates destination,
		bool canMove = false
	)
		=> Try(
			() =>
			{
				_ = Guard.Against.Null(game);
				game = this.CheckAndRewardBoatTriumph(game, destination);
				return canMove || CanMoveTo(game, destination)
					? this.StandardMoveTo(game, destination, true)
						.Match(
							newGameState => newGameState,
							exception => throw exception
						)
					: throw new InvalidOperationException(
						$"{this} cannot move to {destination}."
					);
			}
		);

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