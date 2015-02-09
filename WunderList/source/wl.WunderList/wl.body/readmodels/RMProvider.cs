using System;
using System.Collections.Generic;
using System.Dynamic;
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
                    sw.WriteLine(String.Format("{0},{1},{2}", tm.Id, tm.Name, tm.NumberOfTasks));
                }
            }
        }
        
        public IEnumerable<dynamic> DePersist()
        {
            var serializedRM = File.ReadAllLines(this.filename);
            foreach (string serializedTm in serializedRM) {
                yield return CreateTM(serializedTm);
            }
        }

        dynamic CreateTM(string serializedTM)
        {
            string[] tmInfo = serializedTM.Split(',');
            dynamic tm = new ExpandoObject();
            tm.Id = tmInfo[0];
            tm.Name = tmInfo[1];
            tm.NumberOfTasks = tmInfo[2];
            return tm;
        }
    }
}
