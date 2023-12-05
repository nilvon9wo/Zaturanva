using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public interface IPiece
{
	public Color Color { get; set; }
	public Option<Coordinates> Location { get; set; }
	public bool CanMoveTo(GameState game, Coordinates destination);

	public Try<GameState> MoveTo(GameState game, Coordinates destination);

	public IPlayer Owner { get; set; }

	public Option<Color> CapturedBy { get; set; }
}