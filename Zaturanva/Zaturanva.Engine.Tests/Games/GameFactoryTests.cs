using LanguageExt;

using Zaturanva.Common.Armies;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Pieces;
using Zaturanva.Engine.Games;
using Zaturanva.Engine.Tests.TestUtilities;

namespace Zaturanva.Engine.Tests.Games;

public class GameFactoryTests
{
	[Fact]
	public void CreateFor_ShouldCreateGameWithCorrectArmies()
	{
		// Arrange
		List<IPlayer> players = new()
		{
			new Player().Assign(Color.Black),
			new Player().Assign(Color.White),
			new Player().Assign(Color.Blue),
			new Player().Assign(Color.Orange),
		};

		// Act
		Try<Game> result = GameFactory.CreateFor(players);

		// Assert
		_ = result.Match(
			game =>
			{
				Assert.NotNull(game.BlackArmy);
				Assert.NotNull(game.WhiteArmy);
				Assert.NotNull(game.BlueArmy);
				Assert.NotNull(game.OrangeArmy);

				AssertCorrectArmy(
					game.BlackArmy,
					GameConfiguration.BlackArmyPlacements
				);
				AssertCorrectArmy(
					game.WhiteArmy,
					GameConfiguration.WhiteArmyPlacements
				);
				AssertCorrectArmy(
					game.BlueArmy,
					GameConfiguration.BlueArmyPlacements
				);
				AssertCorrectArmy(
					game.OrangeArmy,
					GameConfiguration.OrangeArmyPlacements
				);
			},
			ex => Assert.True(false, $"Unexpected exception: {ex}")
		);
	}

	private static void AssertCorrectArmy(
		Army army,
		List<string> expectedPiecePlacements
	)
	{
		Assert.NotNull(army);

		foreach (string expectedPlacement in expectedPiecePlacements)
		{
			Option<IPiece> piece = army.GetPieceAt(expectedPlacement);
			_ = piece.Match(
				actualPiece =>
				{
					AssertLocation(
						expectedPlacement.ToUpperInvariant(),
						actualPiece
					);
					Assert.IsType(
						GameConfiguration.GetExpectedPieceType(
							army,
							expectedPlacement
						),
						actualPiece
					);
				},
				() => throw new InvalidDataException(
					"No piece at the expected location."
				)
			);
		}
	}

	private static void AssertLocation(
		string expectedPlacement,
		IPiece actualPiece
	)
		=> actualPiece.Location.Match(
			Some: actualLocation => Assert.Equal(
				expectedPlacement,
				actualLocation
			),
			None: () => throw new InvalidDataException(
				"Piece not at expected location."
			)
		);
}