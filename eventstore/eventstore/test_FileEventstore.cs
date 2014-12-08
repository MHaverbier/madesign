using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eventstore
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void Record ()
		{
			Directory.Delete ("teststore", true);
			var sut = new FileEventstore ("teststore");

			var e = new Event{ ContextId = "1", Name ="e1", Payload ="hello\nworld" };
			sut.Record (e);
			Assert.AreEqual (1, e.Sequencenumber);
			Assert.IsTrue (File.Exists ("teststore/000000001.txt"));
		}


		[Test]
		public void Replay()
		{
			Directory.Delete ("teststore", true);
			var sut = new FileEventstore ("teststore");

			var e = new Event{ ContextId = "c1", Name ="e1", Payload ="hello\nworld" };
			sut.Record (e);

			e = new Event{ ContextId = "c2", Name ="e2", Payload ="42" };
			sut.Record (e);

			var events = sut.Replay ().ToArray ();
			Assert.AreEqual (2, events.Length);

			Assert.AreEqual (1, events [0].Sequencenumber);
			Assert.AreEqual ("c1", events [0].ContextId);
			Assert.AreEqual ("e1", events [0].Name);
			Assert.AreEqual ("hello\nworld", events [0].Payload);

			Assert.AreEqual (2, events [1].Sequencenumber);
			Assert.AreEqual ("c2", events [1].ContextId);
			Assert.AreEqual ("e2", events [1].Name);
			Assert.AreEqual ("42", events [1].Payload);		
		}
	}
}