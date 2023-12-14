namespace Zaturanva.Common.ChessBoard.Geometry;

internal static class SquareChecker
{
	private const double _floatPointErrorTolerance
		= 0.00001;

	internal static bool AreSquare(
		IEnumerable<Coordinates> coordinatesEnumerable
	)
	{
		IEnumerable<double> distances =
			from i in Enumerable.Range(0, 4)
			from j in Enumerable.Range(i + 1, 4 - i - 1)
			select CalculateDistance(coordinatesEnumerable.ToList(), i, j);

		return IsSquare(distances);
	}

	private static bool IsSquare(IEnumerable<double> distances)
	{
		double[] sortedDistances = distances.Order()
			.ToArray();
		return (Math.Abs(sortedDistances[0] - sortedDistances[1])
				< _floatPointErrorTolerance)
			   && (Math.Abs(sortedDistances[1] - sortedDistances[2])
				   < _floatPointErrorTolerance)
			   && (Math.Abs(sortedDistances[2] - sortedDistances[3])
				   < _floatPointErrorTolerance)
			   && (Math.Abs(sortedDistances[4] - sortedDistances[5])
				   < _floatPointErrorTolerance)
			   && (Math.Abs(sortedDistances[4] - (2 * sortedDistances[0]))
				   < _floatPointErrorTolerance);
	}

	private static double CalculateDistance(
		IReadOnlyList<Coordinates> coordinatesList,
		int i,
		int j
	)
	{
		double dx = coordinatesList[i].X - coordinatesList[j].X;
		double dy = coordinatesList[i].Y - coordinatesList[j].Y;
		return Math.Sqrt((dx * dx) + (dy * dy));
	}
}