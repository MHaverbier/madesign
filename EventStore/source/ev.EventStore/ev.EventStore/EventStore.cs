using System.Collections;
using System.Collections.Generic;

namespace ev.EventStore
{
    public class EventStore
    {
        public void Record(Event evt)
        {

        }

        public IEnumerable<Event> Replay()
        {
            yield break;
        }
    }
}