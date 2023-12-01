using LanguageExt;

using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants;
using Zaturanva.Common.Tests.TestUtilities;

namespace Zaturanva.Common.Tests.Contestants;

public class PlayersFactoryTests
{
	[Fact]
	public void FourDistinctPlayersAssignsDifferentColors()
	{
		// Arrange
		List<IPlayer> distinctPlayers = new()
		{
			new Player(),
			new Player(),
			new Player(),
			new Player(),
		};

		// Act
		Try<Players> result = distinctPlayers.ToPlayers();

		// Assert
		int distinctPlayerCount = distinctPlayers.Count;
		_ = result.Match(
			players =>
			{
				Assert.Equal(distinctPlayerCount, players.Count());
				Assert.Equal(distinctPlayerCount, players.CountUniqueColors());
				Assert.Equal(
					distinctPlayerCount,
					players.Sum(player => player.Colors.Count)
				);
				Assert.True(players.All(player => player.Colors.Count == 1));
			},
			ex => Assert.True(false, $"Unexpected exception: {ex}")
		);
	}

	[Fact]
	public void TwoDistinctPlayersAssignsTwoColorsEach()
	{
		// Arrange
		Player player1 = new();
		Player player2 = new();
		List<IPlayer> allPlayers = new()
		{
			player1,
			player2,
			player1,
			player2,
		};

		// Act
		Try<Players> result = allPlayers.ToPlayers();

		// Assert
		_ = result.Match(
			players =>
			{
				int allPlayersCount = allPlayers.Count;
				Assert.Equal(allPlayersCount, players.Count());

				Assert.True(players.All(player => player.Colors.Count == 2));
				Assert.True(
					players.HasPlayerWithColors(ColorUtility.Achromatics)
				);
				Assert.True(players.HasPlayerWithColors(ColorUtility.Vivids));

				List<IPlayer> distinctPlayers = new() { player1, player2 };
				Assert.Equal(
					allPlayersCount,
					distinctPlayers.Sum(player => player.Colors.Count)
				);
			},
			ex => Assert.True(false, $"Unexpected exception: {ex}")
		);
	}

	[Fact]
	public void ThreeDistinctPlayersAssignsTwoColorsToSamePlayer()
	{
		// Arrange
		Player player1 = new();
		Player player2 = new();
		Player player3 = new();
		List<IPlayer> allPlayers = new()
		{
			player1,
			player2,
			player3,
			player1,
		};

		// Act
		Try<Players> result = allPlayers.ToPlayers();

		// Assert
		_ = result.Match(
			players =>
			{
				int allPlayersCount = allPlayers.Count;
				Assert.Equal(allPlayersCount, players.Count());

				Assert.Equal(2, player1.Colors.Count);
				Assert.True(
					player1.Colors.SetEquals(ColorUtility.Achromatics)
					|| player1.Colors.SetEquals(ColorUtility.Vivids)
				);

				_ = Assert.Single(player2.Colors);
				_ = Assert.Single(player3.Colors);
				Assert.Equal(4, players.CountUniqueColors());
			},
			ex => Assert.True(false, $"Unexpected exception: {ex}")
		);
	}
}