﻿// ReSharper disable MemberCanBePrivate.Global

using LanguageExt;
using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;

namespace Zaturanva.Common.Games;

public static class GameStateFactory
{
	public static Try<GameState> CreateFor(IEnumerable<IPlayer> inputPlayers)
		=> inputPlayers
			.ToPlayers()
			.Map(CreateFor);

	private static GameState CreateFor(Players players)
	{
		Army blackArmy = ArmyFactory.CreateFor(players, Color.Black);
		Army whiteArmy = ArmyFactory.CreateFor(players, Color.White);
		Army blueArmy = ArmyFactory.CreateFor(players, Color.Blue);
		Army orangeArmy = ArmyFactory.CreateFor(players, Color.Orange);
		IEnumerable<Pieces.IPiece> allPieces = blackArmy.Pieces
			.Concat(whiteArmy.Pieces)
			.Concat(blueArmy.Pieces)
			.Concat(orangeArmy.Pieces);
		return new GameState()
			{
				Players = players, Board = Board.From(allPieces),
			}
			.SetBlackArmy(blackArmy)
			.SetBlueArmy(blueArmy)
			.SetOrangeArmy(orangeArmy)
			.SetWhiteArmy(whiteArmy);
	}
}