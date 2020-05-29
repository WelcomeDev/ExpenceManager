using System;

namespace DiagramModel
{
	public interface IStringOutputData
	{
		void OutputData(Action<string, string> OutputHandler);
	}
}