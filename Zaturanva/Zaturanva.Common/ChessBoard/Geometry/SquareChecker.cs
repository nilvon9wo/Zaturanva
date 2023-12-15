namespace Zaturanva.Common.ChessBoard.Geometry;

public static class SquareChecker
{
	public static bool AreSquare(IEnumerable<Coordinates> coordinatesEnumerable)
	{
		Coordinates[] coordinatesArray = coordinatesEnumerable.ToArray();
		if (coordinatesArray.Length != 4)
		{
			return false;
		}

		HashSet<int> xValues = coordinatesArray
			.Select(coordinates => coordinates.X)
			.ToHashSet();
		if (!AreTwoAdjacent(xValues))
		{
			return false;
		}

		HashSet<int> yValues = coordinatesArray
			.Select(coordinates => coordinates.Y)
			.ToHashSet();

		return AreTwoAdjacent(yValues);
	}

	private static bool AreTwoAdjacent(IReadOnlyCollection<int> values)
		=> (values.Count == 2)
		   && ((values.Max() - values.Min()) == 1);
}