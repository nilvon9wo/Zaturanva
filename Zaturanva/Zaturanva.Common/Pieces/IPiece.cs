using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public interface IPiece : IColored
{
	public Option<Coordinates> Location { get; }

	public IPlayer Owner { get; init; }

	public Option<Color> CapturedBy { get; set; }

	public IPiece PlaceAt(Coordinates destination);

	public bool CanMoveTo(GameState game, Coordinates destination);

	public Try<GameState> MoveTo(
		GameState game,
		Coordinates destination,
		bool canMove = false
	);

	public Try<GameState> MakeImprisoned(GameState game, Color captor);
}