using Ardalis.GuardClauses;

using LanguageExt;
using LanguageExt.UnsafeValueAccess;

using System.Diagnostics.CodeAnalysis;

using Zaturanva.Common.Colors;
using Zaturanva.Common.Games;
using Zaturanva.Common.Pieces;

using static LanguageExt.Prelude;

namespace Zaturanva.Common.ChessBoard;

public record Board
{
	private Board(
		Dictionary<Coordinates, Cell> cellByCoordinates,
		Dictionary<Color, Coordinates> thronesByColor
	)
	{
		_cellByCoordinates = cellByCoordinates;
		_thronesByColor = thronesByColor;
	}

	private readonly Dictionary<Coordinates, Cell> _cellByCoordinates;
	private readonly Dictionary<Color, Coordinates> _thronesByColor;

	[SuppressMessage(
		"Design",
		"CA1043:Use Integral Or String Argument For Indexers",
		Justification = "<Pending>"
	)]
	public Option<Cell> this[Coordinates coordinates]
		=> _cellByCoordinates.TryGetValue(coordinates, out Cell cell)
			? Option<Cell>.Some(cell)
			: Option<Cell>.None;

	public static Board From(IEnumerable<IPiece> pieces)
	{
		IPiece[] piecesArray = Guard.Against.Null(pieces)
			.ToArray();
		return new(InitializeBoard(piecesArray), CollectThrones(piecesArray));
	}

	private static Dictionary<Color, Coordinates> CollectThrones(
		IEnumerable<IPiece> piecesArray
	)
		=> piecesArray
			.Where(piece => piece is Raja)
			.Aggregate(
				new Dictionary<Color, Coordinates>(),
				(throneDictionary, piece) =>
				{
					_ = piece.Location.IfSome(
						location => throneDictionary[piece.Color] = location
					);
					return throneDictionary;
				}
			);

	private static Dictionary<Coordinates, Cell> InitializeBoard(
		IEnumerable<IPiece> piecesArray
	)
	{
		Dictionary<Coordinates, Cell> populatedCellsByLocation
			= CollectPopulatedLocations(piecesArray);

		return Enumerable
			.Range(0, 8)
			.SelectMany(GenerateRowEntries(populatedCellsByLocation))
			.ToDictionary(pair => pair.Key, pair => pair.Value);
	}

	private static Func<int, IEnumerable<KeyValuePair<Coordinates, Cell>>>
		GenerateRowEntries(
			// ReSharper disable once SuggestBaseTypeForParameter
			Dictionary<Coordinates, Cell> populatedCellsByLocation
		)
		=> row => Enumerable.Range(0, 8)
			.Select(column => new Coordinates(row, column))
			.Select(GenerateCoordinateEntry(populatedCellsByLocation));

	private static Func<Coordinates, KeyValuePair<Coordinates, Cell>>
		GenerateCoordinateEntry(
			// ReSharper disable once SuggestBaseTypeForParameter
			Dictionary<Coordinates, Cell> populatedCellsByLocation
		)
		=> currentCoordinate
			=> new(
				currentCoordinate,
				CreateCell(populatedCellsByLocation, currentCoordinate)
			);

	private static Cell CreateCell(
		// ReSharper disable once SuggestBaseTypeForParameter
		Dictionary<Coordinates, Cell> populatedCellsByLocation,
		Coordinates currentCoordinate
	)
		=> new(
			currentCoordinate,
			populatedCellsByLocation.TryGetValue(
				currentCoordinate,
				out Cell cellAtCoordinate
			)
				? cellAtCoordinate.Piece
				: Option<IPiece>.None
		);

	private static Dictionary<Coordinates, Cell> CollectPopulatedLocations(
		IEnumerable<IPiece> piecesArray
	)
		=> piecesArray
			.Aggregate(
				new Dictionary<Coordinates, Cell>(),
				(coordinateDictionary, piece) =>
				{
					_ = piece.Location.IfSome(
						location => coordinateDictionary[location] = new(
							location,
							Option<IPiece>.Some(piece)
						)
					);
					return coordinateDictionary;
				}
			);

	public Coordinates GetThroneLocation(Color pieceColor)
		=> _thronesByColor[pieceColor];

	// ReSharper disable once MemberCanBePrivate.Global
	public IEnumerable<Cell> GetAllCells()
		=> _cellByCoordinates.Values.ToList();

	public IEnumerable<IPiece> GetAllPieces()
		=> GetAllCells()
			.Where(cell => cell.Piece.IsSome)
			.Select(cell => cell.Piece.ValueUnsafe());

	// ReSharper disable once MemberCanBePrivate.Global
	public IEnumerable<IPiece> GetPieces(Color color)
		=> GetAllPieces()
			.Where(piece => piece.Color == color);

	internal Try<Board> Move(
		GameState game,
		IPiece movingPiece,
		Coordinates destination
	)
		=> Remove(movingPiece)
			.Bind(
				_ => movingPiece.MoveTo(game, destination, true)
					.IsSucc()
					? MoveTo(movingPiece, destination)
					: throw new InvalidOperationException(
						$"{this} cannot move to {destination}."
					)
			)
			.Map(_ => this);

	private Try<Board> MoveTo(
		IPiece piece,
		Coordinates destination
	)
		=> Try(
			() => _cellByCoordinates.TryGetValue(
				destination,
				out Cell destinationCell
			)
				? destinationCell.Piece.Match(
					_ => throw new ArgumentException(
						$"Can't move to occupied cell at {destination}.",
						nameof(destination)
					),
					() =>
					{
						_cellByCoordinates[destination] = new(
							destination,
							Option<IPiece>.Some(piece)
						);
						return this;
					}
				)
				: throw new ArgumentException(
					$"Destination {destination} does not exist.",
					nameof(destination)
				)
		);

	internal Try<Board> Remove(IPiece piece)
		=> Try(
			() =>
			{
				_ = piece.Location.Match(
					location => _cellByCoordinates[location]
						= new(location, Option<IPiece>.None),
					() => throw new ArgumentException(
						"Can't remove piece without location.",
						nameof(piece)
					)
				);
				return this;
			}
		);
}