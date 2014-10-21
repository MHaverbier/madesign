using System;
using System.Collections.Generic;

namespace wortfrequenzen
{

	class CommandlinePortal {
		string[] args;

		public CommandlinePortal(string[] args) {
			this.args = args;
		}

		public string Get_filepath() {
			return this.args[0];
		}
	}
	
}
