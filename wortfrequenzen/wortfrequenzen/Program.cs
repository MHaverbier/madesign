using System;
using System.Collections.Generic;
using System.Linq;

namespace wortfrequenzen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var cmd = new CommandlinePortal ();
			var txt = new TxtProvider ();
			var dom = new Domain ();
			var con = new ConsolePortal ();

			new App (cmd, txt, dom, con)
			   .Run ();
		}
	}


}
