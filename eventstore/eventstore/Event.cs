using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace eventstore
{

	public class Event {
		public long Sequencenumber;
		public string ContextId;
		public string Name;
		public string Payload;
	}

}
