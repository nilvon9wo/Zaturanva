using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Zaturanva.Common.ChessBoard;

public readonly struct Coordinates(int x, int y) : IEquatable<Coordinates>
{
	private int _x { get; } = x;
	private int _y { get; } = y;

	public override bool Equals(object? obj)
		=> obj is Coordinates otherCoordinates
		   && Equals(otherCoordinates);

	public bool Equals(Coordinates other)
		=> (_x == other._x)
		   && (_y == other._y);

	public override int GetHashCode()
		=> (_x.GetHashCode() * 17) + (_y.GetHashCode() * 17);

	public static bool operator ==(Coordinates left, Coordinates right)
		=> left.Equals(right);

	public static bool operator !=(Coordinates left, Coordinates right)
		=> !(left == right);

	private static readonly CultureInfo _invariantCulture = CultureInfo.InvariantCulture;

	[SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "<Pending>")]
	public static implicit operator Coordinates(string coordinateString)
	{
		if (string.IsNullOrEmpty(coordinateString)
			|| (coordinateString.Length != 2))
		{
			throw new InvalidCoordinatesException("Invalid coordinate string");
		}

		int y = int.Parse(coordinateString[1..], _invariantCulture);
		int x = char.ToUpper(coordinateString[0], _invariantCulture) - 'A';
		return new(x, y - 1);
	}

	public static implicit operator string(Coordinates coordinates)
		=> coordinates.ToString();

	public override string ToString()
		=> $"{(char)('A' + _x)}{_y + 1}";

	public Coordinates ToCoordinates()
		=> new(_x, _y);
}