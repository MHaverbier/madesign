using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wl.body.readmodels
{
    public class RMProvider
    {
        string filename;

        public RMProvider(string dirpath)
        {
            this.filename = Path.Combine(dirpath, "ReadModel.txt");
            Directory.CreateDirectory(dirpath);
        }

        public void IsInitialized(Action OnInitialized, Action OnNotIntialized)
        {
            if (File.Exists(this.filename))
            {
                OnInitialized();
            }
            else
            {
                OnNotIntialized();
            }
        }

        public void Persist(IEnumerable<dynamic> tms)
        {
            using (var sw = new StreamWriter(filename))
            {
                foreach (var tm in tms)
                {
                    sw.WriteLine(String.Format("{},{},{}", tm.Id, tm.Name, tm.NumberOfTasks));
                }
            }
        }

        public IEnumerable<dynamic> DePersist()
        {
            var serializedRM = File.ReadAllLines(this.filename);
            yield return null;
        }

        dynamic CreateTM(string serializedTM)
        {
            return null;
        }
    }
}
