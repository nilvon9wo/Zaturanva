using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Zaturanva.Common.Armies;

public class Alliance : IEnumerable<Army>
{
	[SuppressMessage("Style", "IDE0028:Simplify collection initialization", Justification = "<Pending>")]
	private readonly HashSet<Army> _armies = new();

	public IEnumerator<Army> GetEnumerator()
		=> throw new NotImplementedException();

	IEnumerator IEnumerable.GetEnumerator()
		=> throw new NotImplementedException();

	public void Add(Army army)
		=> _armies.Add(army);
}