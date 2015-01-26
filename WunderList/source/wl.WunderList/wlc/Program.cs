using System;
using System.Collections.Generic;
using eventstore;
using wl.body;

namespace wlc
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStore = new FileEventstore("./myStore");
            var repository = new Repository(eventStore);
            var body = new Body(repository);

            switch (args[0])
            {
                case "addList":
                    var listName = args[1];
                    var listId = body.AddList(listName);
                    Console.WriteLine(listId);
                    break;
                case "showLists":
                    IEnumerable<dynamic> taskLists = body.ShowLists();
                    foreach (var list in taskLists)
                    {
                        Console.WriteLine("{0} - {1} - ({2})", list.Id, list.Name, list.NumberOfTasks);
                    }
                    break;
            }
        }
    }
}
