using System;
using wl.body;

namespace wlc
{
    class Program
    {
        static void Main(string[] args)
        {
            var body = new Body();

            switch (args[0])
            {
                case "addList":
                    var listName = args[1];
                    var listId = body.AddList(listName);
                    Console.WriteLine(listId);
                    break;
            }
        }
    }
}
