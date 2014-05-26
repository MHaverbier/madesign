using System.Collections.Generic;
using System.Windows;

namespace it.PoClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public IEnumerable<IssueInfo> IssueInfos { get; set; }
    }
}
