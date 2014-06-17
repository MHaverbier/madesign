using System;
using System.Windows;
using System.Windows.Controls;

namespace it.HLClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TextBox _textBox;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Description Property
        /// <summary>
        /// Property Description	
        /// </summary>
        public string Description
        {
            get { return (string) GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof (string), typeof (MainWindow), new UIPropertyMetadata(string.Empty));
        #endregion

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SubmitButtonClickEvent(Description);
        }

        public event Action<string> SubmitButtonClickEvent;
    }
}
