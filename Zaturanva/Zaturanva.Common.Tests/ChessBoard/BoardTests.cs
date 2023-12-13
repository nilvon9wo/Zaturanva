using LanguageExt;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Pieces;

using Generic = System.Collections.Generic;

namespace Zaturanva.Common.Tests.ChessBoard;

public class BoardTests
{
	[Fact]
	public void ThronesInExpectedPlaces()
	{
		Board board = CreateTestBoard();

		Assert.Equal(
			"E8",
			board.GetThroneLocation(Color.Black)
		);
		Assert.Equal(
			"D1",
			board.GetThroneLocation(Color.White)
		);
	}

	[Fact]
	public void EmptyCellsBetweenA1AndH8()
	{
		IPiece[] pieces = CreateTwoRajas()
			.ToArray();
		Board board = CreateTestBoard(pieces);

		for (int row = 0; row < 8; row++)
		{
			for (int col = 0; col < 8; col++)
			{
				Coordinates coordinates = new(row, col);
				if (((row == 0) && (col == 0))
					|| ((row == 7) && (col == 7)))
				{
					continue;
				}

				_ = board[coordinates]
					.Match(
						cell =>
						{
							bool isOccupied = cell.Piece.IsSome;
							if (isOccupied)
							{
								Assert.Fail(
									$"Cell {coordinates} should be empty"
								);
							}
						},
						() => Assert.False(
							false,
							$"Cell {coordinates} should not be faulted"
						)
					);
			}
		}
	}

	private static List<IPiece> CreateTwoRajas()
		=> new()
		{
			new Raja(Color.White, new Player().Assign(Color.White))
			{
				Location = Option<Coordinates>.Some("a1"),
			},
			new Raja(Color.Black, new Player().Assign(Color.Black))
			{
				Location = Option<Coordinates>.Some("h8"),
			},
		};

	[Fact]
	public void NoMissingPieces()
	{
		IPiece[] pieces = CreatePieces()
			.ToArray();
		Board board = CreateTestBoard(pieces);
		Generic.HashSet<IPiece> piecesOnBoard = board
			.GetAllPieces()
			.ToHashSet();

		Assert.Equal(pieces.Length, piecesOnBoard.Count);
		Assert.All(
			pieces,
			expectedPiece => Assert.Contains(expectedPiece, piecesOnBoard)
		);
	}

	private static Board CreateTestBoard(IEnumerable<IPiece>? pieces = null)
	{
		pieces ??= CreatePieces();
		return Board.From(pieces);
	}

	private static IEnumerable<IPiece> CreatePieces()
	{
		Players players = new()
		{
			new Player().Assign(Color.Black),
			new Player().Assign(Color.Blue),
			new Player().Assign(Color.Orange),
			new Player().Assign(Color.White),
		};
		Army blackArmy = ArmyFactory.CreateFor(players, Color.Black);
		Army whiteArmy = ArmyFactory.CreateFor(players, Color.White);
		Army blueArmy = ArmyFactory.CreateFor(players, Color.Blue);
		Army orangeArmy = ArmyFactory.CreateFor(players, Color.Orange);
		return blackArmy.Pieces
			.Concat(whiteArmy.Pieces)
			.Concat(blueArmy.Pieces)
			.Concat(orangeArmy.Pieces);
	}
}