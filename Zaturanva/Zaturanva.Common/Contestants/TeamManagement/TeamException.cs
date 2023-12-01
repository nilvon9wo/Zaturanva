namespace Zaturanva.Common.Contestants.TeamManagement;

public class TeamException : Exception
{
	public TeamException()
		: base()
	{
	}

	public TeamException(string? message)
		: base(message)
	{
	}

	public TeamException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}