using System.Collections.Generic;
using eventstore;

namespace wl.body.readmodels
{
    public class RMManager
    {
        private readonly RMProvider _rmProvider;
        private readonly RMBuilder _rmBuilder;
        private readonly FileEventstore _eventStore;
        private readonly RMMapper _rmMapper;

        public RMManager(RMProvider rmProvider, RMBuilder rmBuilder, RMMapper rmMapper, FileEventstore eventstore)
        {
            _rmProvider = rmProvider;
            _rmBuilder = rmBuilder;
            _rmMapper = rmMapper;
            _eventStore = eventstore;
        }

        public IEnumerable<dynamic> Read()
        {
            IEnumerable<dynamic> result = null;

            _rmProvider.IsInitialized(
                () => { result =_rmProvider.DePersist(); },
                () => { result = Initialize(); });

            return result;
        }

        public void Update(Event e)
        {
            var persistedTMs = _rmProvider.DePersist();
            var tms2 = _rmBuilder.Build(e, persistedTMs);
            _rmProvider.Persist(tms2);
        }

        public IEnumerable<dynamic> Initialize()
        {
            var tm = _rmBuilder.Build(_eventStore.Replay());
            _rmProvider.Persist(tm);
            return tm;
        }
    }
}
