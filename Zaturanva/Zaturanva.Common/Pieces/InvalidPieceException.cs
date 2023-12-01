namespace Zaturanva.Common.Pieces;

public class InvalidPieceException : Exception
{
	public InvalidPieceException(string message)
		: base(message)
	{
	}

	public InvalidPieceException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	public InvalidPieceException()
		: base()
	{
	}
}