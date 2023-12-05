using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public interface IPiece
{
	public Color Color { get; }
	public Option<Coordinates> Location { get; set; }
	public bool CanMoveTo(Game game, Coordinates destination);

	public Try<Game> MoveTo(Game game, Coordinates destination);

	public IPlayer Owner { get; set; }

	public Option<IPlayer> CapturedBy { get; set; }
}