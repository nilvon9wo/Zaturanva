using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Zaturanva.Common.ChessBoard;

public readonly struct Coordinates(int x, int y) : IEquatable<Coordinates>
{
	public int X { get; } = x;
	public int Y { get; } = y;

	public override bool Equals(object? obj)
		=> obj is Coordinates otherCoordinates
		   && Equals(otherCoordinates);

	public bool Equals(Coordinates other)
		=> (X == other.X)
		   && (Y == other.Y);

	public override int GetHashCode()
		=> (X.GetHashCode() * 17) + (Y.GetHashCode() * 17);

	public static bool operator ==(Coordinates left, Coordinates right)
		=> left.Equals(right);

	public static bool operator !=(Coordinates left, Coordinates right)
		=> !(left == right);

	private static readonly CultureInfo _invariantCulture
		= CultureInfo.InvariantCulture;

	[SuppressMessage(
		"Design",
		"CA1065:Do not raise exceptions in unexpected locations",
		Justification = "<Pending>"
	)]
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

	public static Coordinates operator -(Coordinates left, Coordinates right)
		=> Subtract(left, right);

	// ReSharper disable once MemberCanBePrivate.Global
	public static Coordinates Subtract(Coordinates left, Coordinates right)
		=> new(left.X - right.X, left.Y - right.Y);

	public static Coordinates operator +(Coordinates left, Coordinates right)
		=> Add(left, right);

	// ReSharper disable once MemberCanBePrivate.Global
	public static Coordinates Add(Coordinates left, Coordinates right)
		=> new(left.X + right.X, left.Y + right.Y);

	public static implicit operator string(Coordinates coordinates)
		=> coordinates.ToString();

	public override string ToString()
		=> $"{(char)('A' + X)}{Y + 1}";

	public Coordinates ToCoordinates()
		=> new(X, Y);

	public Coordinates Rotate(int angle)
	{
		int x = X;
		int y = Y;

		int normalizedAngle = ((angle % 360) + 360) % 360;
		return normalizedAngle switch
		{
			0 => new(x, y),
			90 => new(y, 7 - x),
			180 => new(7 - x, 7 - y),
			270 => new(7 - y, x),
			_ => throw new ArgumentException(
				$"Invalid angle: {angle}. Only 0, 90, 180, and 270 are supported."
			),
		};
	}
}