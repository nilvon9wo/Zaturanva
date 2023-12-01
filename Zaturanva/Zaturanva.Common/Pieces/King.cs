﻿using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Contestants;

namespace Zaturanva.Common.Pieces;

public class King : IPiece
{
	public required IPlayer Owner { get; set; }

	public Option<IPlayer> CapturedBy { get; set; } = Option<IPlayer>.None;

	public required Option<Coordinates> Location { get; set; }

	public bool CanBeMovedBy(Board board, IPlayer player)
		=> throw new NotImplementedException();

	public bool CanMoveTo(Board board, Coordinates destination)
		=> throw new NotImplementedException();

	public Try<IPiece> MoveTo(Board board, Coordinates destination)
		=> throw new NotImplementedException();
}