using System.Collections;

namespace Zaturanva.Common.Armies;

public class Alliance : IEnumerable<Army>
{
	private readonly HashSet<Army> _armies = new();

	public IEnumerator<Army> GetEnumerator()
		=> throw new NotImplementedException();

	IEnumerator IEnumerable.GetEnumerator()
		=> throw new NotImplementedException();

	public void Add(Army army)
		=> _armies.Add(army);
}