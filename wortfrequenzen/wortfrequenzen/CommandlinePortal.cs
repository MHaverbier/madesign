using System;
using System.Collections.Generic;

namespace wortfrequenzen
{

	class CommandlinePortal {
		public string Get_filepath() {
			return Environment.GetCommandLineArgs () [1];
		}
	}
	
}
