using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Armies;

// ReSharper disable once ClassNeverInstantiated.Global
public class Army
{
	public required Color Color { get; init; }
	public required IPlayer Owner { get; set; }
	public required HashSet<IPiece> Pieces { get; init; }
}