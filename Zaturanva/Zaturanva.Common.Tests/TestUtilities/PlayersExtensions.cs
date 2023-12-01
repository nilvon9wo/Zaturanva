﻿using LanguageExt;

using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants;

using Generic = System.Collections.Generic;

namespace Zaturanva.Common.Tests.TestUtilities;

public static class PlayersExtensions
{
	public static int CountUniqueColors(this Players players)
	{
		ArgumentNullException.ThrowIfNull(players);

		return players.Aggregate(
				new Generic.HashSet<Color>(),
				(uniqueColors, player) =>
				{
					uniqueColors.UnionWith(player.Colors);
					return uniqueColors;
				}
			)
			.Count;
	}

	public static bool HasPlayerWithColors(
		this Players players,
		Generic.HashSet<Color> targetColors
	)
		=> (
				from IPlayer player in players
				where player.Colors.SetEquals(targetColors)
				select new { }
			)
			.Any();
}