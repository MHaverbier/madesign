using System;
using System.Collections.Generic;
using System.Linq;
using eventstore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wl.body.readmodels;

namespace wl.body.Test
{
    [TestClass]
    public class ReadModelBuilderTest
    {

        [TestMethod]
        public void ListCreated_CreatesASingle_Tm()
        {
            var builder = new RMBuilder();

            var @events = new[] { new Event() { ContextId = "1", Name = "ListCreated", Payload = "Listen Name", Sequencenumber = 0L } };
            var result = builder.Build(@events).ToArray();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void OnlyActivatedTasksAreCounted()
        {
            var builder = new RMBuilder();

            var @events = new[]
            {
                "ListCreated".Create("1", "Listen Name"),
                "TaskAddedToList".Create("1", "123" ),
                "ListCreated".Create("2", "Listen B"),
                "TaskAddedToList".Create("1", "4711" ),
                "TaskAddedToList".Create("1", "42" ),
                "TaskAddedToList".Create("2", "41" ),
                "TaskActivated".Create("4711"),
                "TaskAddedToList".Create("2", "43" ),
                "TaskActivated".Create("123"),
                "TaskDeactivated".Create("4711"),
                "TaskDeactivated".Create("41"),
                "TaskActivated".Create("42"),
            };
            var result = builder.Build(@events).ToArray();

            Assert.AreEqual(2, result.Length, "Expected one list item");

            Assert.AreEqual("1", result[0].ListId, "List Id mismatch");
            Assert.AreEqual("Listen Name", result[0].ListName, "List Name mismatch");
            Assert.AreEqual(2, result[0].TaskIds.Count(t => t.IsActive), "Activated task mismatch.");

            Assert.AreEqual("2", result[1].ListId, "List Id mismatch");
            Assert.AreEqual("Listen B", result[1].ListName, "List Name mismatch");
            Assert.AreEqual(1, result[1].TaskIds.Count(t => t.IsActive), "Activated task mismatch.");
        }
    }
}
