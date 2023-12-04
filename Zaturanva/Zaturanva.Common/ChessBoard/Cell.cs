using LanguageExt;

using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.ChessBoard;

internal readonly struct Cell(Coordinates coordinates, Option<IPiece> piece)
{
	public Coordinates Coordinates { get; } = coordinates;
	public Option<IPiece> Piece { get; } = piece;
}