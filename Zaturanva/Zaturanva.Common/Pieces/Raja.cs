﻿using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;

namespace Zaturanva.Common.Pieces;

public class Raja : IPiece
{
	public Color Color { get; init; }

	public required IPlayer Owner { get; set; }

	public Option<Color> CapturedBy { get; set; } = Option<Color>.None;

	public required Option<Coordinates> Location { get; set; }

	public bool CanMoveTo(GameState game, Coordinates destination)
		=> throw new NotImplementedException();

	public Try<GameState> MoveTo(GameState game, Coordinates destination)
		=> throw new NotImplementedException();
}