namespace Zaturanva.Common.ChessBoard;

public class InvalidCoordinatesException : Exception
{
	public InvalidCoordinatesException(string message)
		: base(message)
	{
	}

	public InvalidCoordinatesException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	public InvalidCoordinatesException()
		: base()
	{
	}
}