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
    /// Interaction logic for User_add_edit.xaml
    /// </summary>
    public partial class User_add_edit : Window
    {
        private User user;
        private AptekaDataDataContext con = new AptekaDataDataContext();
        private int mode = 0;
        public User_add_edit(User user,int mode)
        {
            InitializeComponent();
            this.user = user;
            this.mode = mode;
        }
        public User_add_edit()
        {
            InitializeComponent();
            this.user = new User();
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            if (Nazwa_użytkownika.Text == "")
                MessageBox.Show("Nazwa użytkownika nie moża być pusta!");
            else
            {
                 if (Haslo.Password == "")
                    MessageBox.Show("Musi być podane hasłó !");
                else
                {
                    user.Nazwa_uzytkownika = Nazwa_użytkownika.Text.Trim();
                    user.Haslo = Haslo.Password;
                    if (Prawo.SelectedIndex == 0)
                        user.Prawo = "A";
                    else
                        user.Prawo = "U";
                    if (mode == 1)
                    {
                        Admin.EditUser(user);
                        MessageBox.Show("Dane zostałe zmienione!");
                        this.DialogResult = true;
                    }
                    else
                    {
                        User us = (from u in con.Users where u.Nazwa_uzytkownika == Nazwa_użytkownika.Text select u).FirstOrDefault();
                        if (us != null)
                            MessageBox.Show("Taki użytkownik już istnieje!");
                        else
                        {
                            Admin.AddUser(user);
                            MessageBox.Show("Użytkownik " + Nazwa_użytkownika.Text + " został dodany!");
                            this.DialogResult = true;
                        }
                    }
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (mode == 1)
            {
                Nazwa_użytkownika.Text = user.Nazwa_uzytkownika;
                Haslo.Password = user.Haslo;
                if (user.Prawo == "A") Prawo.SelectedIndex = 0;
                else
                    Prawo.SelectedIndex = 1;
            }
            
    
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
