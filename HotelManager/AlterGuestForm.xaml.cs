using DataManagement;
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
    /// Interaction logic for AlterGuestForm.xaml
    /// </summary>
    public partial class AlterGuestForm : Window
    {
        private IGuest target;
        public AlterGuestForm(IGuest target)
        {
            InitializeComponent();
            this.target = target;
            txtName.Text = target.Name;
            txtEmail.Text = target.Email;
            txtPhoneNumber.Text = target.Phone_Number;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckData.CheckGuestData(txtName.Text, txtEmail.Text, txtPhoneNumber.Text))
                {
                    throw new Exception("incorrect format");
                }
                else
                {
                    target.Name = txtName.Text;
                    target.Email = txtEmail.Text;
                    target.Phone_Number = txtPhoneNumber.Text;
                }
                DataManipulation.AlterGuestAsync(target);
                DataManipulation.UpdateNameInReservationsAsync(target.Guest_Id, target.Name);
                MessageBox.Show("Changes saved");
                this.Close();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
