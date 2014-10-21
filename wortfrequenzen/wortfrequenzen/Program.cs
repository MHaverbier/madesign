using System;
using System.Collections.Generic;
using System.Linq;

namespace wortfrequenzen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var cmd = new CommandlinePortal (args);
			var txt = new TxtProvider ();
			var dom = new Domain ();
			var con = new ConsolePortal ();

			var filepath = cmd.Get_filepath ();
			var text = txt.Load_file_content (filepath);
			var frequencies = dom.Count_frequencies (text);
			con.Display_frequencies (frequencies);
		}
	}
}
