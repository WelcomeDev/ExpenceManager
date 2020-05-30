using System.Collections.Generic;

namespace DiagramModel
{
	//TODO: Remove it
	public abstract class EnumerableType<T>
							where T : IEnumType
	{
		protected List<T> types;

		public IEnumerable<T> GetTypes()
		{
			foreach(var item in types)
			{
				yield return item;
			}
		}
	}
}
