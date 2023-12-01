using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants;

namespace Zaturanva.Common.Pieces;

public interface IPiece
{
	public Option<Coordinates> Location { get; set; }
	public bool CanMoveTo(Board board, Coordinates destination);

	public Try<IPiece> MoveTo(Board board, Coordinates destination);

	public IPlayer Owner { get; set; }

	public bool CanBeMovedBy(Board board, IPlayer player);

	public Option<IPlayer> CapturedBy { get; set; }
}