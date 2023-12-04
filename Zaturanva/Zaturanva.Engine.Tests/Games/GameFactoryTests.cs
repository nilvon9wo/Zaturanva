using LanguageExt;

using Zaturanva.Common.Armies;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Pieces;
using Zaturanva.Engine.Games;

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
					"h8",
					"g8",
					"f8",
					"e8",
					"h7",
					"g7",
					"f7",
					"e7"
				);
				AssertCorrectArmy(
					game.WhiteArmy,
					"a1",
					"b1",
					"c1",
					"d1",
					"a2",
					"b2",
					"c2",
					"d2"
				);
				AssertCorrectArmy(
					game.BlueArmy,
					"a8",
					"a7",
					"a6",
					"a5",
					"b8",
					"b7",
					"b6",
					"b5"
				);
				AssertCorrectArmy(
					game.OrangeArmy,
					"h1",
					"h2",
					"h3",
					"h4",
					"g1",
					"g2",
					"g3",
					"g4"
				);
			},
			ex => Assert.True(false, $"Unexpected exception: {ex}")
		);
	}

	private static void AssertCorrectArmy(
		Army army,
		params string[] expectedPiecePlacements
	)
	{
		Assert.NotNull(army);
		foreach (string expectedPlacement in expectedPiecePlacements)
		{
			Type expectedPieceType
				= GetExpectedPieceType(
					expectedPlacement
				); // Adjust the expected piece type based on your configuration
			Option<IPiece> piece = army.GetPieceAt(expectedPlacement);
			_ = piece.Match(
				actualPiece =>
				{
					AssertLocation(
						expectedPlacement.ToUpperInvariant(),
						actualPiece
					);
					Assert.IsType(expectedPieceType, actualPiece);
				},
				() => throw new InvalidDataException(
					"No piece not at expected location."
				)
			);
		}
	}

	private static Type GetExpectedPieceType(string placement)
	{
		int placementIndex = Array.IndexOf(_expectedPiecePlacements, placement);
		return _pieceTypes[placementIndex % _pieceTypes.Count];
	}

	private static readonly List<Type> _pieceTypes = new()
	{
		typeof(Boat),
		typeof(Horse),
		typeof(Elephant),
		typeof(King),
		typeof(Pawn),
		typeof(Pawn),
		typeof(Pawn),
		typeof(Pawn),
	};

	private static readonly string[] _expectedPiecePlacements = new string[]
	{
		"h8",
		"g8",
		"f8",
		"e8",
		"h7",
		"g7",
		"f7",
		"e7",
		"a1",
		"b1",
		"c1",
		"d1",
		"a2",
		"b2",
		"c2",
		"d2",
		"a8",
		"a7",
		"a6",
		"a5",
		"b8",
		"b7",
		"b6",
		"b5",
		"h1",
		"h2",
		"h3",
		"h4",
		"g1",
		"g2",
		"g3",
		"g4",
	};

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