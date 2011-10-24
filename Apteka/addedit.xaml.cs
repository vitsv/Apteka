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
    /// Interaction logic for addedit.xaml
    /// </summary>
    public partial class addedit : Window
    {
        private Lek lek;
        private AptekaDataDataContext con = new AptekaDataDataContext();
        private int mode = 0; //
        public addedit(Lek lek, int mode)
        {
            InitializeComponent();
            this.lek = lek;
            this.mode = mode;
        }
        public addedit()
        {
            InitializeComponent();
            this.lek = new Lek();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Kategorie> kat = (from k in con.Kategories select k).ToList();
            Kategoria.ItemsSource = kat;
            Kategoria.DisplayMemberPath = "Nazwa";
            Kategoria.SelectedValuePath = "KategorieID";
            List<Postac> pos = (from p in con.Postacs select p).ToList();
            Postac.ItemsSource = pos;
            Postac.DisplayMemberPath = "Nazwa";
            Postac.SelectedValuePath = "PostacID";

            if (mode == 1)
            {
                Nazwa.Text = lek.Nazwa;
                M_nazwa.Text = lek.M_nazwa;
                Kategoria.SelectedValue = lek.KategorieID.ToString();
                Postac.SelectedValue = lek.PostacID.ToString();
                Cena.Text = lek.Cena.ToString();
                Cena_h.Text = lek.Cena_hutowa.ToString();
                if (lek.Refundacja != 0)
                {
                    Refcheck.IsChecked = true;
                    Refundacja.Text = lek.Refundacja.ToString();
                }
                Waznosc.Text = lek.Data_waznosci.ToShortDateString();
                Dawka.Text = lek.Dawka.ToString();
                Opakowanie.Text = lek.Opakowanie.ToString();
                if (lek.Promocja != 0)
                {
                    Promcheck.IsChecked = true;
                    Promocja.Text = lek.Promocja.ToString();
                }
                Ilosc.Text = lek.Ilosc.ToString();
            }
     }

        private void textBox6_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Kategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            
        }



        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            if (Nazwa.Text == "" || M_nazwa.Text == "")
                MessageBox.Show("Musi być podana nazwa leku!");
            else if (Kategoria.Text == "" || Postac.Text == "")
                MessageBox.Show("Musi być podana kategorja i postać!");
            else if (Cena.Text == "" || Cena_h.Text == "")
                MessageBox.Show("Musi być podana cena!");
            else if (Waznosc.Text == "")
                MessageBox.Show("Musi być podana data ważności!");
            else
            {
                lek.Nazwa = Nazwa.Text;
                lek.M_nazwa = M_nazwa.Text;
                lek.KategorieID = Convert.ToInt32(Kategoria.SelectedValue.ToString());
                lek.PostacID = Convert.ToInt32(Postac.SelectedValue.ToString());
                lek.Cena = Convert.ToDouble(Cena.Text);
                lek.Cena_hutowa = Convert.ToDouble(Cena_h.Text);
                lek.Data_waznosci = Convert.ToDateTime(Waznosc.Text);
                if (Opakowanie.Text == "") Opakowanie.Text = "0";
                lek.Opakowanie = Convert.ToInt32(Opakowanie.Text);
                if (Dawka.Text == "") Dawka.Text = "0";
                lek.Dawka = Convert.ToInt32(Dawka.Text);
                if (Promcheck.IsChecked == true && Promocja.Text != "")
                    lek.Promocja = Convert.ToDouble(Promocja.Text);
                if (Refcheck.IsChecked == true && Refundacja.Text != "")
                    lek.Refundacja = Convert.ToDouble(Refundacja.Text);
                lek.Ilosc = Convert.ToInt32(Ilosc.Text);
                if (mode == 1)
                {
                    Admin.UpdateLek(lek);
                    MessageBox.Show("Dane zostałe zmienione!");
                    this.DialogResult = true;

                }
                else
                {
                    Admin.AddLek(lek);
                    MessageBox.Show("Nowa pozycja zostałą dodana do bazy!");
                    this.DialogResult = true;
                }
            }
        }

        private void Analuj_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Refcheck_Checked(object sender, RoutedEventArgs e)
        {

                Refundacja.IsEnabled = true;
        }

        private void Promcheck_Checked(object sender, RoutedEventArgs e)
        {
                Promocja.IsEnabled = true;
        }

        private void Refcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Refundacja.IsEnabled = false;
            Refundacja.Text = "";
        }

        private void Promcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Promocja.Text = "";
            Promocja.IsEnabled = false;
        }



    }
}
