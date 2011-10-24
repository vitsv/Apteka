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
    /// Interaction logic for ViewBase.xaml
    /// </summary>
    public partial class ViewBase : Window
    {
        public AddItemDelegate AddItemCallback;
        public int search = 0; //ta zmienna pokazuje czy trzeba pokazywac wyniki wyszukiwania przekazane z okna "Szukaj"
        AptekaDataDataContext con = new AptekaDataDataContext(); //tworze połączenie tutaj, zeby nie robic pozniej dwa raza
        private int mode = 0;
        public ViewBase(int mode)
        {
            InitializeComponent();
            this.mode = mode;
        }

        public ViewBase()
        {
            InitializeComponent();
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Jezeli nie bylo wyszukiwania to poprostu pokaż całą bazę
            if (search == 0)
            {
                List<Lek> lek = (from l in con.Leks select l).ToList();
                LekGird.ItemsSource = lek;
            }
            if (mode == 1)
                Order.Visibility = System.Windows.Visibility.Visible;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //tak tez mozna zamknąc to okno
            this.DialogResult = false;
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Przy zmianach tekstu w polu "Nazwa" odswierz dane wedlug wprowadzonego tekstu
            List<Lek> lek = (from l in con.Leks where l.Nazwa.StartsWith(textBox1.Text) || l.M_nazwa.StartsWith(textBox1.Text) select l).ToList();
            LekGird.ItemsSource = lek;
        }

        private void Additional_Click(object sender, RoutedEventArgs e)
        {
            Lek selected = LekGird.SelectedItem as Lek;
            if (selected == null)
                MessageBox.Show("Nie wybrano żadnej pozycji!");
            else
            {
                addedit ae = new addedit(selected, 1);
                ae.Nazwa.IsReadOnly = true;
                ae.M_nazwa.IsReadOnly = true;
                ae.Kategoria.IsEnabled = false;
                ae.Postac.IsEnabled = false;
                ae.Cena.IsReadOnly = true;
                ae.Cena_h.IsReadOnly = true;
                ae.Dawka.IsReadOnly = true;
                ae.Refcheck.IsEnabled = false;
                ae.Refundacja.IsReadOnly = true;
                ae.Promcheck.IsEnabled = false;
                ae.Promocja.IsReadOnly = true;
                ae.Waznosc.IsReadOnly = true;
                ae.Zapisz.IsEnabled = false;
                ae.Opakowanie.IsReadOnly = true;
                ae.ShowDialog();
                Window_Loaded(null, null);
            }
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Lek selected = LekGird.SelectedItem as Lek;
            if (selected == null)
                MessageBox.Show("Nie wybrano żadnej pozycji!");
            else
            {
                if (selected.Ilosc > 0)
                {
                    AddItemCallback(selected);
                    this.DialogResult = true;
                }
                else
                    MessageBox.Show("W tej chwili leku nie ma!");
            }
        }

    }
}
 