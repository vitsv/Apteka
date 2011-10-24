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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Apteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dzisiaj.Content = DateTime.Today.ToLongDateString();

        }

        private void Window_Activated(object sender, EventArgs e)
        {
           // this.Hide();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //Przycisk Menu->Wyloguj
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            //Tworze nowe okno dla zalogowania, i zamykam to.
            Login logowanie = new Login();
            logowanie.Show();
            this.Close();
        }
        //Przycisk Menu->Przyglądaj bazę
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            //Tworze okno "Przyglądaj bazę"
            ViewBase viewbase = new ViewBase();
            viewbase.ShowDialog();
        }
        //Przycisk Menu->Szukaj
        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {
            //Tworze okno "Szukaj"
            Search search = new Search();
            search.ShowDialog();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Lekedit lekedit = new Lekedit();
            lekedit.mode = 1;
            lekedit.ShowDialog();

        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            addedit ae = new addedit();
            ae.ShowDialog();
            Window_Loaded(null, null);
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            Accounts acc = new Accounts();
            acc.ShowDialog();
        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            ZamWindow zam = new ZamWindow();
            zam.ShowDialog();
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            DostWindow dw = new DostWindow();
            dw.ShowDialog();
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            magazyn mag = new magazyn();
            mag.ShowDialog();
        }

    }
}
