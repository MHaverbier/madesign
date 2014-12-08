using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ev.EventStore
{
    public class EventStore
    {
        private int aktuelleSequenzNummer;

        public int Record(Event evt)
        {
            var neueSequenzNummer = aktuelleSequenzNummer + 1;
            
            var dateiName = string.Format("Event-{0:000000000000}", neueSequenzNummer);

            using (var datei =  new StreamWriter(dateiName))
            {
                datei.WriteLine(neueSequenzNummer);
                datei.WriteLine(evt.ContextId);
                datei.WriteLine(evt.EventName);
                datei.Write(evt.Payload);
            }

            aktuelleSequenzNummer = neueSequenzNummer;
            return neueSequenzNummer;
        }

        public IEnumerable<Event> Replay()
        {
        
            yield break;
        }
    }
}