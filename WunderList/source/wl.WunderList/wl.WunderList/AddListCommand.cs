using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace wl.WunderList
{
    public class AddListCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        private HeadViewModel headViewModel;
        private body.Body body;

        public AddListCommand(HeadViewModel headViewModel, body.Body body)
        {
            this.headViewModel = headViewModel;
            this.body = body;
        }

        public void Execute(object parameter)
        {
            body.AddList((string) parameter);
            headViewModel.Lists = body.ShowLists();
        }
    }
}
