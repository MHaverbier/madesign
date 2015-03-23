using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wl.WunderList
{
    public class HeadViewModel : INotifyPropertyChanged
    {

        public HeadViewModel(body.Body body)
        {
            this.AddListCommand = new AddListCommand(this, body);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private IEnumerable<dynamic> _lists;

        public IEnumerable<dynamic> Lists
        {
            get { return _lists; }
            set { _lists = value; NotifyPropertyChanged("Lists"); }
        }

        private ICommand _addListCommand;

        public ICommand AddListCommand
        {
            get { return _addListCommand; }
            set { _addListCommand = value; NotifyPropertyChanged("AddListCommand"); }
        }
        
        
    }

}
