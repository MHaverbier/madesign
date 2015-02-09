using System.Collections.Generic;
using eventstore;

namespace wl.body.readmodels
{
    public class RMManager
    {
        private readonly RMProvider _rmProvider;

        public RMManager(RMProvider rmProvider)
        {
            _rmProvider = rmProvider;
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
            
        }

        public IEnumerable<dynamic> Initialize()
        {
            return null;
        }
    }
}
