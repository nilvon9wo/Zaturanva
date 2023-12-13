using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class Raja(Color color, IPlayer owner) : IPiece
{
	public Color Color { get; init; } = color;

	public IPlayer Owner { get; init; } = owner;

	public Option<Color> CapturedBy { get; set; } = Option<Color>.None;

	public required Option<Coordinates> Location { get; set; }

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
		return (xDifference <= 1) && (yDifference <= 1);
	}
}