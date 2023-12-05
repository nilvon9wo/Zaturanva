using System.Collections;

namespace Zaturanva.Common.Armies;

public class Alliance : IEnumerable<Army>
{
	private readonly HashSet<Army> _armies = new();

	public IEnumerator<Army> GetEnumerator()
		=> _armies.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public void Add(Army army)
		=> _armies.Add(army);
}