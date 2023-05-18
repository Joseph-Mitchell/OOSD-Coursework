using System.Windows;

namespace Student_Records_System
{
    public partial class ValidInput : Window
    {
        public ValidInput()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}