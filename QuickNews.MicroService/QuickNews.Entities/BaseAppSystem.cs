using QuickNews.Utilities;

namespace QuickNews.Entities
{
	public class BaseAppSystem
	{
		public LogManager Log;
		public BaseAppSystem(LogManager log) { Log = log; }
	}
}
