using LanguageExt;

namespace Zaturanva.Common.ChessBoard;

internal static class OptionCellExtensions
{
	public static bool IsOccupied(this Option<Cell> cellOption)
		=> cellOption.Match(
			cell => cell.Piece.IsSome,
			false
		);

	public static bool IsVacant(this Option<Cell> cellOption)
		=> !cellOption.IsOccupied();
}