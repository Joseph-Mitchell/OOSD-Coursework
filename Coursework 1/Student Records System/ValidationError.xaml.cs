using System.Windows;

namespace Student_Records_System
{
    public partial class ValidationError : Window
    {
        public ValidationError(string e)
        {
            InitializeComponent();

            lbl_error.Content = e;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}