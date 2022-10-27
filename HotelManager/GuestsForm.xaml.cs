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
using static DataManagement.DataManipulation;

namespace HotelManager;

/// <summary>
/// Interaction logic for GuestsForm.xaml
/// </summary>
public partial class GuestsForm : Window
{
    public GuestsForm()
    {
        InitializeComponent();
    }

    private async void btnSeach_Click(object sender, RoutedEventArgs e)
    {
        cmbGuests.ItemsSource = null;
        cmbGuests.ItemsSource = await GetGuestsAsync(txtName.Text);
        cmbGuests.DisplayMemberPath = "Display";
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        NewGuestForm f = new NewGuestForm();
        f.ShowDialog();
    }

    private void btnChange_Click(object sender, RoutedEventArgs e)
    {
        if (cmbGuests.SelectedItem != null)
        {
            AlterGuestForm f = new AlterGuestForm((GuestModel)cmbGuests.SelectedItem);
            f.ShowDialog();
            cmbGuests.ItemsSource = null;
        }
        else MessageBox.Show("please select guest");
    }
}
