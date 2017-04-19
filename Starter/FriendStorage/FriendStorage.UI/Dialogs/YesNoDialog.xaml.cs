using System.Windows;

namespace FriendStorage.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for YesNoDialog.xaml
    /// </summary>
    public partial class YesNoDialog : Window
    {
        public YesNoDialog(string message, string title)
        {
            InitializeComponent();
            Title = title;
            textBlock.Text = message;
        }

        private void ButtonYes_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ButtonNo_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
