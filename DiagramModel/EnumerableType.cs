using System.Collections.Generic;

namespace DiagramModel
{
	public abstract class EnumerableType<T>
							where T : IEnumType
	{
		protected List<T> types;

		public IEnumerator<T> GetTypes()
		{
			return types.GetEnumerator();
		}
	}
}
