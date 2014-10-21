using System;
using System.Collections.Generic;
using System.Linq;

namespace wortfrequenzen
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var c = new SimpleInjector.Container ();
			c.Register<IFilepathAdapter> (() => new CommandlineMock ());
			c.GetInstance<App> ()
			 .Run ();
		}
	}


	class CommandlineMock : IFilepathAdapter {
		#region IFilepathAdapter implementation

		public string Get_filepath ()
		{
			return "beispieltext.txt";
		}

		#endregion
	}
}
