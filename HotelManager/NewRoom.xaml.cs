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
    /// Interaction logic for NewRoom.xaml
    /// </summary>
    public partial class NewRoom : Window
    {
        public NewRoom()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int roomNum = int.Parse(txtBeds.Text);
                DataManagement.DataManipulation.SaveRoomAsync(txtRoomNum.Text, roomNum);
                MessageBox.Show("Saved");
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Please insert number of beds properly");
            }
            catch (Exception ex)
            {
               MessageBox.Show("Invalid insert please check if room already exsisits and if you filled all the fields","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        }
    }
