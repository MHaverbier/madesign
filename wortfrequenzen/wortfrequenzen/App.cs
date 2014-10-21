using System;
using System.Collections.Generic;
using System.Linq;

namespace wortfrequenzen
{

	class App {
		IFilepathAdapter cmd;
		TxtProvider txt;
		Domain dom;
		ConsolePortal con;

		public App(IFilepathAdapter cmd, TxtProvider txt, Domain dom, ConsolePortal con) {
			this.con = con;
			this.dom = dom;
			this.txt = txt;
			this.cmd = cmd;
		}

		public void Run() {
			var filepath = cmd.Get_filepath ();
			var text = txt.Load_file_content (filepath);
			var frequencies = dom.Count_frequencies (text);
			con.Display_frequencies (frequencies);
		}
	}
}
