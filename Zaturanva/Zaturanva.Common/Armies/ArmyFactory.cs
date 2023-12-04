using Ardalis.GuardClauses;

using LanguageExt;

using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Pieces;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.Armies;

public static class ArmyFactory
{
	private static readonly Dictionary<Coordinates, Type>
		_blackPieceTypeByInitialPlacement = new()
		{
			{ "h8", typeof(Boat) },
			{ "g8", typeof(Horse) },
			{ "f8", typeof(Elephant) },
			{ "e8", typeof(Raja) },
			{ "h7", typeof(Pawn) },
			{ "g7", typeof(Pawn) },
			{ "f7", typeof(Pawn) },
			{ "e7", typeof(Pawn) },
		};

	private static readonly Dictionary<Coordinates, Type>
		_whitePieceTypeByInitialCoordinates = new()
		{
			{ "a1", typeof(Boat) },
			{ "b1", typeof(Horse) },
			{ "c1", typeof(Elephant) },
			{ "d1", typeof(Raja) },
			{ "a2", typeof(Pawn) },
			{ "b2", typeof(Pawn) },
			{ "c2", typeof(Pawn) },
			{ "d2", typeof(Pawn) },
		};

	private static readonly Dictionary<Coordinates, Type>
		_bluePieceTypeByInitialCoordinates = new()
		{
			{ "a8", typeof(Boat) },
			{ "a7", typeof(Horse) },
			{ "a6", typeof(Elephant) },
			{ "a5", typeof(Raja) },
			{ "b8", typeof(Pawn) },
			{ "b7", typeof(Pawn) },
			{ "b6", typeof(Pawn) },
			{ "b5", typeof(Pawn) },
		};

	private static readonly Dictionary<Coordinates, Type>
		_orangePieceTypeByInitialCoordinates
			= new()
			{
				{ "h1", typeof(Boat) },
				{ "h2", typeof(Horse) },
				{ "h3", typeof(Elephant) },
				{ "h4", typeof(Raja) },
				{ "g1", typeof(Pawn) },
				{ "g2", typeof(Pawn) },
				{ "g3", typeof(Pawn) },
				{ "g4", typeof(Pawn) },
			};

	private static readonly Dictionary<Color, Dictionary<Coordinates, Type>>
		_pieceTypeByInitialCoordinatesByColor = new()
		{
			{ Color.Black, _blackPieceTypeByInitialPlacement },
			{ Color.Blue, _bluePieceTypeByInitialCoordinates },
			{ Color.Orange, _orangePieceTypeByInitialCoordinates },
			{ Color.White, _whitePieceTypeByInitialCoordinates },
		};

	public static Army CreateForBlack(Players players)
		=> CreateFor(players, Color.Black);

	public static Army CreateForWhite(Players players)
		=> CreateFor(players, Color.White);

	public static Army CreateForBlue(Players players)
		=> CreateFor(players, Color.Blue);

	public static Army CreateForOrange(Players players)
		=> CreateFor(players, Color.Orange);

	private static Army CreateFor(Players players, Color color)
		=> Create(
				Guard.Against.Null(players)[color],
				color,
				_pieceTypeByInitialCoordinatesByColor[color]
			)
			.Match(
				army => army,
				ex => throw ex
			);

	private static Try<Army> Create(
		IPlayer owner,
		Color color,
		Dictionary<Coordinates, Type> pieceTypeByInitialCoordinates
	)
		=> Try<Army>(
			() => new()
			{
				Owner = Guard.Against.Null(owner),
				Color = color,
				Pieces = Guard.Against.Null(pieceTypeByInitialCoordinates)
					.Select(
						kv => PieceFactory.Create(owner, kv)
							.Match(
								piece => piece,
								exception => throw exception
							)
					)
					.ToHashSet(),
			}
		);
}