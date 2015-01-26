namespace wl.body
{
    public class Body
    {
        public string AddList(string listName)
        {
            var repository = new Repository();
            return repository.AddList(listName);
        }
    }
}
