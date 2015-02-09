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

        public void Check_for_readmodel_existence(Action OnInitialized, Action OnNotIntialized)
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

        public void Persist(IEnumerable<ListRM> rms)
        {
            using (var sw = new StreamWriter(filename))
            {
                foreach (var rm in rms)
                {
                    sw.WriteLine("{0},{1},{2}", rm.ListId, rm.ListName, string.Join(",", rm.TaskIds.Select(t => string.Format("{0},{1}", t.TaskId, t.IsActive))));
                }
            }
        }
        
        public IEnumerable<ListRM> DePersist()
        {
            if (!File.Exists(this.filename))
            {
                yield break;
            }
            else
            {
                var serializedRM = File.ReadAllLines(this.filename);
                foreach (string serializedTm in serializedRM)
                {
                    yield return CreateRM(serializedTm);
                }
            }
        }

        private ListRM CreateRM(string serializedTM)
        {
            string[] rmInfo = serializedTM.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var rm = new ListRM(rmInfo[0], rmInfo[1]);
            for (var i = 2; i < rmInfo.Length; i += 2)
            {
                rm.TaskIds.Add(new ListRM.TaskRm { TaskId = rmInfo[i], IsActive = bool.Parse(rmInfo[i+1]) });
            }
            return rm;
        }
    }
}
