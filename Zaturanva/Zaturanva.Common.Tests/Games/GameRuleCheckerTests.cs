using LanguageExt;
using LanguageExt.UnsafeValueAccess;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Games;

public static class GameRuleCheckerTests
{
	private static readonly Coordinates _targetLocation = "c3";

	[Fact]
	public static void
		IsMoveAllowedByStandardRules_DestinationDoesNotExist_ReturnsFalse()
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = false,
				AllowActiveColorSelfCapture = false,
				AllowAllyCapture = false,
			}
		);
		IPiece piece = game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act
		bool result
			= game.IsMoveAllowedByStandardRules(piece, new(10, 10));

		// Assert
		Assert.False(result);
	}

	[Fact]
	public static void
		IsMoveAllowedByStandardRules_DestinationExistsButIsEmpty_ReturnsTrue()
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = false,
				AllowActiveColorSelfCapture = false,
				AllowAllyCapture = false,
			}
		);
		IPiece piece = game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act
		bool result = game.IsMoveAllowedByStandardRules(piece, _targetLocation);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(
		Color.White,
		Color.Black,
		false,
		false
	)]
	[InlineData(
		Color.White,
		Color.Black,
		true,
		false
	)]
	[InlineData(
		Color.White,
		Color.White,
		true,
		false
	)]
	[InlineData(
		Color.White,
		Color.White,
		false,
		false
	)]
	[InlineData(
		Color.White,
		Color.Blue,
		false,
		true
	)]
	[InlineData(
		Color.White,
		Color.Blue,
		true,
		true
	)]
	[InlineData(
		Color.White,
		Color.Orange,
		false,
		true
	)]
	[InlineData(
		Color.White,
		Color.Orange,
		true,
		true
	)]
	[InlineData(
		Color.Blue,
		Color.Black,
		false,
		true
	)]
	[InlineData(
		Color.Blue,
		Color.Black,
		true,
		true
	)]
	[InlineData(
		Color.Blue,
		Color.White,
		true,
		true
	)]
	[InlineData(
		Color.Blue,
		Color.White,
		false,
		true
	)]
	[InlineData(
		Color.Blue,
		Color.Blue,
		false,
		false
	)]
	[InlineData(
		Color.Blue,
		Color.Blue,
		true,
		false
	)]
	[InlineData(
		Color.Blue,
		Color.Orange,
		false,
		false
	)]
	[InlineData(
		Color.Blue,
		Color.Orange,
		true,
		false
	)]
	public static void
		IsMoveAllowedByStandardRules_DestinationExistsContainsActiveColor_ReturnsExpectedResult(
			Color activeColor,
			Color targetColor,
			bool allowColorSelfCapture,
			bool expectedResult
		)
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = false,
				AllowActiveColorSelfCapture = allowColorSelfCapture,
				AllowMovingColorSelfCapture = true,
				AllowAllyCapture = false,
			},
			targetColor
		);
		game.ActiveColor = activeColor;
		IPiece piece = game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act

		bool result
			= game.IsMoveAllowedByStandardRules(piece, _targetLocation);

		// Assert
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(Color.Black, false, false)]
	[InlineData(Color.Black, true, true)]
	[InlineData(Color.White, false, false)]
	[InlineData(Color.White, true, false)]
	[InlineData(Color.Blue, false, true)]
	[InlineData(Color.Blue, true, true)]
	[InlineData(Color.Orange, false, true)]
	[InlineData(Color.Orange, true, true)]
	public static void
		IsMoveAllowedByStandardRules_DestinationExistsContainsAlly_ReturnsExpectedResult(
			Color targetColor,
			bool allowAllySelfCapture,
			bool expectedResult
		)
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = false,
				AllowActiveColorSelfCapture = false,
				AllowAllyCapture = allowAllySelfCapture,
			},
			targetColor
		);
		IPiece piece = game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act

		bool result
			= game.IsMoveAllowedByStandardRules(piece, _targetLocation);

		// Assert
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(
		Color.Black,
		false,
		false,
		false
	)]
	[InlineData(
		Color.Black,
		true,
		false,
		false
	)]
	[InlineData(
		Color.Black,
		false,
		true,
		false
	)]
	[InlineData(
		Color.Black,
		true,
		true,
		false
	)]
	[InlineData(
		Color.Blue,
		false,
		false,
		true
	)]
	[InlineData(
		Color.Blue,
		true,
		false,
		true
	)]
	[InlineData(
		Color.Blue,
		false,
		true,
		true
	)]
	[InlineData(
		Color.Blue,
		true,
		true,
		true
	)]
	[InlineData(
		Color.Orange,
		false,
		false,
		true
	)]
	[InlineData(
		Color.Orange,
		true,
		false,
		true
	)]
	[InlineData(
		Color.Orange,
		false,
		true,
		true
	)]
	[InlineData(
		Color.Orange,
		true,
		true,
		true
	)]
	[InlineData(
		Color.White,
		false,
		false,
		false
	)]
	[InlineData(
		Color.White,
		true,
		false,
		false
	)]
	[InlineData(
		Color.White,
		false,
		true,
		false
	)]
	[InlineData(
		Color.White,
		true,
		true,
		true
	)]
	public static void
		IsMoveAllowedByStandardRules_DestinationExistsContainsOwnPiece_ReturnsExpectedResult(
			Color targetColor,
			bool allowPlayerSelfCapture,
			bool allowActiveColorSelfCapture,
			bool expectedResult
		)
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = allowPlayerSelfCapture,
				AllowActiveColorSelfCapture = allowActiveColorSelfCapture,
				AllowMovingColorSelfCapture = true,
				AllowAllyCapture = false,
			},
			targetColor
		);
		IPiece piece = game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act

		bool result
			= game.IsMoveAllowedByStandardRules(piece, _targetLocation);

		// Assert
		Assert.Equal(expectedResult, result);
	}

	private static GameState SetupGameForNormalMovement(
		GameOptions gameOptions,
		Color? targetColor = null
	)
		=> CreateGameFor(CreatePlayers(), gameOptions, targetColor);

	private static Players CreatePlayers()
		=> new()
		{
			new Player().Assign(Color.Black),
			new Player().Assign(Color.White),
			new Player().Assign(Color.Blue),
			new Player().Assign(Color.Orange),
		};

	private static GameState CreateGameFor(
		Players players,
		GameOptions gameOptions,
		Color? targetColor = null
	)
	{
		Army blackArmy = ArmyFactory.CreateFor(players, Color.Black);
		Army whiteArmy = ArmyFactory.CreateFor(players, Color.White);
		Army blueArmy = ArmyFactory.CreateFor(players, Color.Blue);
		Army orangeArmy = ArmyFactory.CreateFor(players, Color.Orange);
		IEnumerable<IPiece> allPieces = blackArmy.Pieces
			.Concat(whiteArmy.Pieces)
			.Concat(blueArmy.Pieces)
			.Concat(orangeArmy.Pieces);

		if (targetColor != null)
		{
			allPieces = allPieces.Concat(
				new List<IPiece>
				{
					new Pawn()
					{
						Owner = players[(Color)targetColor],
						Color = (Color)targetColor,
						Location = Option<Coordinates>.Some(_targetLocation),
					},
				}
			);
		}

		return new GameState()
		{
			Players = players,
			ActiveColor = Color.White,
			GameOptions = gameOptions,
			Board = Board.From(allPieces),
		}
			.SetBlackArmy(blackArmy)
			.SetBlueArmy(blueArmy)
			.SetOrangeArmy(orangeArmy)
			.SetWhiteArmy(whiteArmy);
	}
}