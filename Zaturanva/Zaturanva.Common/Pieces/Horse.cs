using LanguageExt;
using LanguageExt.UnsafeValueAccess;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Pieces;

public class Horse(Color color, IPlayer owner) : IPiece
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

		return ((xDifference == 2) && (yDifference == 1))
			   || ((xDifference == 1) && (yDifference == 2));
	}

	public Try<GameState> MoveTo(
		GameState game,
		Coordinates destination,
		bool canMove = false
	)
		=> Try(
			() =>
			{
				if (canMove || CanMoveTo(game, destination))
				{
					Location = destination;
					return game;
				}

				throw new InvalidOperationException(
					$"{this} cannot move to {destination}."
				);
			}
		);

	public Try<GameState> MakeImprisoned(GameState game, Color captor)
		=> Try(
			() =>
			{
				if (CapturedBy.IsSome)
				{
					throw new InvalidOperationException(
						$"{this} is already captured by {CapturedBy.ValueUnsafe()}"
					);
				}

				CapturedBy = captor;
				Location = Option<Coordinates>.None;
				return game;
			}
		);
}