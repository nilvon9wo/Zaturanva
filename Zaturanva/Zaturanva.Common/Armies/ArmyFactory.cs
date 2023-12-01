﻿using LanguageExt;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants;
using Zaturanva.Common.Pieces;
using static LanguageExt.Prelude;

namespace Zaturanva.Common.Armies;

public static class ArmyFactory
{
	public static Try<Army> Create(
		IPlayer owner,
		Color color,
		Dictionary<Coordinates, Type> pieceTypeByInitialCoordinates
	)
		=> Try(
			() => new Army()
			{
				Owner = owner,
				Color = color,
				Pieces = pieceTypeByInitialCoordinates
					.Select(
						kv => PieceFactory.Create(owner, kv)
							.Match(
								piece => piece,
								exception => throw exception
							)
					)
					.ToHashSet(),
			}
		);
}