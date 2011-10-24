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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        //Po naciśnieńciu przycisku "Wyjscie" zamykam program
        private void button2_Click(object sender, RoutedEventArgs e) 
        {
            Application.Current.Shutdown();
        }
        //Przycisk "Zaloguj"
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (nazwa_uzytkownika.Text == "" || pass.Password == "") //sprawdzam czy pola nie są puste
                MessageBox.Show("Wprowasz nazwe użytkownika i hasło!");
            else
            {
                AptekaDataDataContext con = new AptekaDataDataContext(); //jezeli pola wypełnione, to podłączam sie do bazy, i sprawdzam czy istnieje taki użytkownik i czy dobre hasło
                User user = (from u in con.GetTable<User>() where (u.Nazwa_uzytkownika == nazwa_uzytkownika.Text && u.Haslo == pass.Password) select u).SingleOrDefault<User>();
                if (user == null) MessageBox.Show("Błędne hasło lub nazwa użytkownika!");
                else
                {
                    //Jeśli Ok, to tworze "Glówne" okno, i zamykam to.
                    MainWindow mw = new MainWindow();
                    mw.username_.Content = nazwa_uzytkownika.Text;
                    if (user.Prawo == "A")
                    {
                        mw.rights_.Content = "(Administrator)";
                        mw.Admin.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                        mw.rights_.Content = "(Użytkownik)";
                    mw.Show();
                    this.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Robie fokus na pole tekstowe z nazwa uzytkownika
            nazwa_uzytkownika.Focus();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
