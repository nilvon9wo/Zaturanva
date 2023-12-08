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
			currentLocation
				=> IsMovementAllowed(currentLocation, destination)
				   && IsDestinationAllowed(game, destination),
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

	private bool IsDestinationAllowed(
		GameState game,
		Coordinates destination
	)
		=> game.Board[destination]
			.Match(
				cell => cell.Piece.Match(
					piece =>
					{
						Color pieceColor = piece.Color;
						return (pieceColor != Color)
							   || ((pieceColor == Color)
								   && game.GameOptions.AllowColorSelfCapture)
							   || Color.IsEnemy(pieceColor)
							   || (Color.IsAlly(pieceColor)
								   && game.GameOptions.AllowAllyCapture)
							   || (Owner != piece.Owner)
							   || ((Owner == piece.Owner)
								   && game.GameOptions.AllowPlayerSelfCapture);
					},
					() => true
				),
				() => false
			);
}