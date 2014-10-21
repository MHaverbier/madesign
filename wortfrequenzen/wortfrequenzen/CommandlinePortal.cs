using System;
using System.Collections.Generic;

namespace wortfrequenzen
{
	class CommandlinePortal : IFilepathAdapter {
		public string Get_filepath() {
			return Environment.GetCommandLineArgs () [1];
		}
	}
		
	interface IFilepathAdapter {
		string Get_filepath ();
	}
}
