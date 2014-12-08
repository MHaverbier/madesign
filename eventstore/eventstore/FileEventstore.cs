using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eventstore
{
	public class FileEventstore {
		string dirpath;

		public FileEventstore(string dirpath) {
			this.dirpath = dirpath;
			Directory.CreateDirectory (dirpath);
		}

		public void Record(Event e) {
			e.Sequencenumber = Directory.GetFiles (this.dirpath).Length + 1;
			var filename = Path.Combine (this.dirpath, string.Format ("{0:000000000}.txt", e.Sequencenumber));
			using (var sw = new StreamWriter (filename)) {
				sw.WriteLine (e.Sequencenumber);
				sw.WriteLine (e.ContextId);
				sw.WriteLine (e.Name);
				sw.Write (e.Payload);
			}
			this.OnRecorded (e);
		}

		public IEnumerable<Event> Replay() {
			var eventfilepaths = Directory.GetFiles (this.dirpath);
			foreach (var efp in eventfilepaths) {
				var serializedEvent = File.ReadAllLines (efp);
				yield return Replay (serializedEvent);
			}
		}

		Event Replay(string[] serializedEvent) {
			var e = new Event ();
			e.Sequencenumber = long.Parse (serializedEvent [0]);
			e.ContextId = serializedEvent [1];
			e.Name = serializedEvent [2];
			e.Payload = string.Join ("\n", serializedEvent.Skip (3));
			return e;
		}


		public event Action<Event> OnRecorded = _ => {};
	}
}