using System.IO;
using System.Windows;

namespace Student_Records_System
{
    public partial class MainWindow : Window
    {
        public string[] Countries { get; set; }

        private string error;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Countries = File.ReadAllLines(@"../../countries.txt");
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            //Resets all fields to original values
            txtbx_firstName.Text = "";
            txtbx_surname.Text = "";
            txtbx_age.Text = "";
            cmbobx_course.SelectedItem = null;
            txtbx_address1.Text = "";
            txtbx_address2.Text = "";
            txtbx_city.Text = "";
            txtbx_postcode.Text = "";
            txtbx_email.Text = "";
            chkbx_international.IsChecked = false;
            cmbobx_country.SelectedItem = null;
            Height = 380;
        }

        private void RunValidate(object sender, RoutedEventArgs e)
        {
            if(Validate())
            {
                ValidInput validInput = new ValidInput();
                validInput.ShowDialog();
            }
            else
            {
                ValidationError errorWindow = new ValidationError(error);
                errorWindow.ShowDialog();
            }
        }

        //Called when the Validate button is pressed
        //Checks each field for errors
        private bool Validate()
        {
            error = "";

            //Error if either name field is blank
            if (string.IsNullOrWhiteSpace(txtbx_firstName.Text))
            {
                error = "First name is blank";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtbx_surname.Text))
            {
                error = "Surname is blank";
                return false;
            }

            //Returns false if input to age field cannot convert to an integer
            try
            {
                if(int.Parse(txtbx_age.Text) < 16 || int.Parse(txtbx_age.Text) > 101)
                {
                    error = "Age is out of range";
                    return false;
                }
            }
            catch
            {
                error = "Age is invalid";
                return false;
            }

            //Check for blank text fields
            if (cmbobx_course.SelectedItem == null)
            {
                error = "Course selection is blank";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtbx_address1.Text))
            {
                error = "Address is blank";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtbx_city.Text))
            {
                error = "City is blank";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtbx_postcode.Text))
            {
                error = "Postcode is blank";
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtbx_email.Text))
            {
                error = "Email is blank";
                return false;
            }
            //If text does not contain '@', IndexOf returns -1
            if (txtbx_email.Text.IndexOf('@') == -1)
            {
                error = "Email is invalid";
                return false;
            }
            //Assures that the first and last character of the email field are letters/digits
            if (!char.IsLetterOrDigit(txtbx_email.Text, 0))
            {
                error = "Email is invalid";
                return false;
            }
            if (!char.IsLetterOrDigit(txtbx_email.Text, txtbx_email.Text.Length - 1))
            {
                error = "Email is invalid";
                return false;
            }


            //Error if International Student box is checked but no country is chosen
            if ((bool)chkbx_international.IsChecked && cmbobx_country.SelectedItem == null)
            {
                error = "Country selection is blank";
                return false;
            }

            return true;
        }

        private void ExpandCountry(object sender, RoutedEventArgs e)
        {
            Height += 40;
            lbl_country.Visibility = Visibility.Visible;
            cmbobx_country.Visibility = Visibility.Visible;
        }

        private void CollapseCountry(object sender, RoutedEventArgs e)
        {
            cmbobx_country.SelectedItem = null;
            Height -= 40;
            lbl_country.Visibility = Visibility.Hidden;
            cmbobx_country.Visibility = Visibility.Hidden;
        }
    }
}