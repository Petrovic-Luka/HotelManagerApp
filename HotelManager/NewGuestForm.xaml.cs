using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataManagement;

namespace HotelManager
{
    /// <summary>
    /// Interaction logic for NewGuestForm.xaml
    /// </summary>
    public partial class NewGuestForm : Window
    {
        public NewGuestForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataManipulation.SaveGuestAsync(txtName.Text, txtEmail.Text, txtPhoneNumber.Text);
                MessageBox.Show("Saved");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
