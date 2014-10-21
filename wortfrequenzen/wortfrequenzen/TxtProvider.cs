using System;
using System.Collections.Generic;

namespace wortfrequenzen
{

	class TxtProvider {
		public string Load_file_content(string filepath) {
			return System.IO.File.ReadAllText (filepath);
		}
	}
}
