namespace Zaturanva.Common.Contestants;

public class PlayersException : Exception
{
	public PlayersException(string message)
		: base(message)
	{
	}

	public PlayersException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	public PlayersException()
		: base()
	{
	}
}