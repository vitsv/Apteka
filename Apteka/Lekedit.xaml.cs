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
    /// Interaction logic for Lekedit.xaml
    /// </summary>
    public partial class Lekedit : Window
    {
        AptekaDataDataContext con = new AptekaDataDataContext();
        public int mode;
        public Lekedit()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Lek> lek = (from l in con.Leks select l).ToList();
            LekGird.ItemsSource = lek;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Lek> lek = (from l in con.Leks where l.Nazwa.StartsWith(textBox1.Text) || l.M_nazwa.StartsWith(textBox1.Text) select l).ToList();
            LekGird.ItemsSource = lek;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            addedit ae = new addedit();
            ae.ShowDialog();
            Window_Loaded(null, null);
        }

        private void LekGird_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Edituj_Click(object sender, RoutedEventArgs e)
        {
            Lek selected = LekGird.SelectedItem as Lek;
            if (selected == null)
                MessageBox.Show("Nie wybrano żadnej pozycji!");
            else
            {
                addedit ae = new addedit(selected, 1);
                ae.ShowDialog();
                Window_Loaded(null, null);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Lek selected = LekGird.SelectedItem as Lek;
            if (selected == null)
                MessageBox.Show("Nie wybrano żadnej pozycji!");
            else
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Na pewno chcesz usunąć wybraną pozycją?", "Usunąć", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    Admin.DeleteLek(selected);
                    Window_Loaded(null, null);
                }
            }
        }
    }
}
