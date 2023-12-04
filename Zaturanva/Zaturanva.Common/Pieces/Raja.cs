using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class Raja : IPiece
{
	public Color Color { get; init; }

	public required IPlayer Owner { get; set; }

	public Option<IPlayer> CapturedBy { get; set; } = Option<IPlayer>.None;

	public required Option<Coordinates> Location { get; set; }

	public bool CanBeMovedBy(Game game, IPlayer player)
		=> throw new NotImplementedException();

	public bool CanMoveTo(Game game, Coordinates destination)
		=> throw new NotImplementedException();

	public Try<IPiece> MoveTo(Game game, Coordinates destination)
		=> throw new NotImplementedException();
}