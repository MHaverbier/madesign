using System;
using System.Web;
using System.Web.UI;

namespace magischeerzeugung
{
	public partial class Default : System.Web.UI.Page
	{
		Domain dom;

		public Default() {
			this.dom = new Domain ();
			dom.Begrüßung += this.Begrüßung_anzeigen;
		}


		public void button1Clicked (object sender, EventArgs args)
		{
			dom.Begrüßung_basteln ();
		}


		void Begrüßung_anzeigen(string begrüßung) {
			this.button1.Text = begrüßung;
		}
	}



	class Domain {
		public void Begrüßung_basteln() {
			Begrüßung ("hello @ " + DateTime.Now.ToString ());
		}


		public event Action<string> Begrüßung;
	}
}

