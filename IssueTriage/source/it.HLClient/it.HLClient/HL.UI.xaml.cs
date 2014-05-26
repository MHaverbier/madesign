using System;
using System.Windows;
using System.Windows.Controls;

namespace it.HLClient
{
    /// <summary>
    /// Interaction logic for HL.xaml
    /// </summary>
    public partial class HL : Window
    {
        private readonly TextBox _textBox;

        public HL()
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
            DependencyProperty.Register("Description", typeof (string), typeof (HL), new UIPropertyMetadata(string.Empty));
        #endregion

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SubmitButtonClickEvent(Description);
        }

        public event Action<string> SubmitButtonClickEvent;
    }
}
