using LanguageExt;

using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.ChessBoard;

public readonly struct Cell(Coordinates coordinates, Option<IPiece> piece)
	: IEquatable<Cell>
{
	// ReSharper disable once MemberCanBePrivate.Global
	public Coordinates Coordinates { get; } = coordinates;
	public Option<IPiece> Piece { get; } = piece;

	public override bool Equals(object? obj)
		=> obj is Cell other && Equals(other);

	public bool Equals(Cell other)
		=> Coordinates.Equals(other.Coordinates) && Piece.Equals(other.Piece);

	public override int GetHashCode()
		=> HashCode.Combine(Coordinates, Piece);

	public static bool operator ==(Cell left, Cell right)
		=> left.Equals(right);

	public static bool operator !=(Cell left, Cell right)
		=> !(left == right);
}