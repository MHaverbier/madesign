using System;
using System.Collections.Generic;
using eventstore;
using wl.body;
using wl.body.datamodels;
using wl.body.readmodels;

namespace wlc
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStore = new FileEventstore("./myStore");
            var repository = new Repository(eventStore);
            var rmProvider = new RMProvider("./myRmStore");
            var rmBuilder = new RMBuilder();
            var rmMapper = new RMMapper();
            var readModelManager = new RMManager(rmProvider, rmBuilder, rmMapper, eventStore);
            var body = new Body(repository, readModelManager);
            var head = new Head(body);

            eventStore.OnRecorded += readModelManager.Update;

            head.Run(args);
        }
    }
}
