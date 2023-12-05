using LanguageExt;

using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Pieces;

using static LanguageExt.Prelude;

using Generic = System.Collections.Generic;

namespace Zaturanva.Common.Armies;

// ReSharper disable once ClassNeverInstantiated.Global
public class Army
{
	public required Color Color { get; init; }
	public required IPlayer Owner { get; set; }
	public required Generic.HashSet<IPiece> Pieces { get; init; }

	public Raja Raja
		=> (Raja)Pieces.First(piece => piece is Raja);

	public Option<IPiece> GetPieceAt(string targetLocation)
	{
		IPiece? piece = Pieces
			.FirstOrDefault(
				piece => piece.Location.Match(
					location => location == targetLocation,
					() => false
				)
			);
		return piece == null
			? Option<IPiece>.None
			: Some(piece);
	}
}