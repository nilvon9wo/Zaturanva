using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public interface IPiece
{
	public Color Color { get; init; }
	public Option<Coordinates> Location { get; set; }
	public bool CanMoveTo(GameState game, Coordinates destination);

	public IPlayer Owner { get; init; }

	public Option<Color> CapturedBy { get; set; }
}