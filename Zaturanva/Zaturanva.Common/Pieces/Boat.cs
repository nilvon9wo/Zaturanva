using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class Boat : IPiece
{
	public Color Color { get; set; }

	public required IPlayer Owner { get; set; }

	public Option<Color> CapturedBy { get; set; } = Option<Color>.None;

	public required Option<Coordinates> Location { get; set; }

	public Option<Color> SharedWithForBoatTriumph { get; set; }
		= Option<Color>.None;

	public bool CanMoveTo(GameState game, Coordinates destination)
		=> Location.Match(
			currentLocation =>
			{
				int xDifference = Math.Abs(destination.X - currentLocation.X);
				int yDifference = Math.Abs(destination.Y - currentLocation.Y);
				return (xDifference == 2)
					   && (yDifference == 2)
					   && game.Board[destination].IsSome;
			},
			() => false
		);
}