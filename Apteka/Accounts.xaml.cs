using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Apteka
{
    /// <summary>
    /// Interaction logic for Accounts.xaml
    /// </summary>
    public partial class Accounts : Window
    {
        public Accounts()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AptekaDataDataContext con = new AptekaDataDataContext();
            List<User> us = (from u in con.Users select u).ToList();
            KontaGrid.ItemsSource = us;
        }

        private void Zamknij_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            User_add_edit ae = new User_add_edit();
            ae.ShowDialog();
            Window_Loaded(null, null);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            User selected = KontaGrid.SelectedItem as User;
            if (selected == null)
                MessageBox.Show("Nie wybrano żadnej pozycji!");
            else
            {
                User_add_edit ae = new User_add_edit(selected, 1);
                ae.ShowDialog();
                Window_Loaded(null, null);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            User selected = KontaGrid.SelectedItem as User;
            if (selected == null)
                MessageBox.Show("Nie wybrano żadnej pozycji!");
            else
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Na pewno chcesz usunąć wybraną pozycją?", "Usunąć", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    Admin.DeleteUser(selected);
                    Window_Loaded(null, null);
                }
            }
        }
    }
}
