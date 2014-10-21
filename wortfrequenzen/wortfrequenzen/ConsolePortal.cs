using System;
using System.Collections.Generic;
using System.Linq;

namespace wortfrequenzen
{

	class ConsolePortal {
		public void Display_frequencies(IEnumerable<Tuple<string,int>> frequencies) {
			var result = Map (frequencies);
			Output (result);
		}

		static string Map (IEnumerable<Tuple<string, int>> frequencies)
		{
			frequencies = frequencies.OrderByDescending (f => f.Item2);

			var result = "";
			foreach (var f in frequencies) {
				if (result != "")
					result += "\n";
				result += string.Format ("{0}: {1}", f.Item1, f.Item2);
			}
			return result;
		}

		static void Output (string result)
		{
			Console.WriteLine (result);
		}
	}

}
