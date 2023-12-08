﻿using LanguageExt;
using LanguageExt.UnsafeValueAccess;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

namespace Zaturanva.Common.Tests.Pieces;

public static class PieceUtilityTests
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
				AllowColorSelfCapture = false,
				AllowAllyCapture = false,
			}
		);

		Boat boat = (Boat)game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act
		bool result
			= boat.IsMoveAllowedByStandardRules(
				game,
				new(10, 10)
			);

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
				AllowColorSelfCapture = false,
				AllowAllyCapture = false,
			}
		);

		Boat boat = (Boat)game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act
		bool result = boat.IsMoveAllowedByStandardRules(game, _targetLocation);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(Color.Black, false, true)]
	[InlineData(Color.White, false, false)]
	[InlineData(Color.Blue, false, true)]
	[InlineData(Color.Orange, false, true)]
	[InlineData(Color.Black, true, true)]
	[InlineData(Color.White, true, true)]
	[InlineData(Color.Blue, true, true)]
	[InlineData(Color.Orange, true, true)]
	public static void
		IsMoveAllowedByStandardRules_DestinationExistsContainsOwnColor_ReturnsTrue(
			Color targetColor,
			bool allowColorSelfCapture,
			bool expectedResult
		)
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = true,
				AllowColorSelfCapture = allowColorSelfCapture,
				AllowAllyCapture = true,
			},
			targetColor
		);

		Boat whiteBoat = (Boat)game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act

		bool result
			= whiteBoat.IsMoveAllowedByStandardRules(game, _targetLocation);

		// Assert
		Assert.Equal(expectedResult, result);
	}

	[Theory]
	[InlineData(Color.Black, false, false)]
	[InlineData(Color.White, false, false)]
	[InlineData(Color.Blue, false, true)]
	[InlineData(Color.Orange, false, true)]
	[InlineData(Color.Black, true, true)]
	[InlineData(Color.White, true, false)]
	[InlineData(Color.Blue, true, true)]
	[InlineData(Color.Orange, true, true)]
	public static void
		IsMoveAllowedByStandardRules_DestinationExistsContainsAlly_ReturnsTrue(
			Color targetColor,
			bool allowAllyCapture,
			bool expectedResult
		)
	{
		// Arrange
		GameState game = SetupGameForNormalMovement(
			new()
			{
				AllowPlayerSelfCapture = true,
				AllowColorSelfCapture = false,
				AllowAllyCapture = allowAllyCapture,
			},
			targetColor
		);

		Boat whiteBoat = (Boat)game.Board["a1"]
			.ValueUnsafe()
			.Piece.ValueUnsafe();

		// Act

		bool result
			= whiteBoat.IsMoveAllowedByStandardRules(game, _targetLocation);

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
			GameOptions = gameOptions,
			Board = Board.From(allPieces),
		}
			.SetBlackArmy(blackArmy)
			.SetBlueArmy(blueArmy)
			.SetOrangeArmy(orangeArmy)
			.SetWhiteArmy(whiteArmy);
	}
}