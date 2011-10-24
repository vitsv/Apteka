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
    /// Interaction logic for DostWindow.xaml
    /// </summary>
    //public delegate void AddItemDelegate(Lek item);
    public partial class DostWindow : Window
    {
         AptekaDataDataContext con = new AptekaDataDataContext();
        private class Pozycja
        {
            public Lek lekitem;
            public int ile;
        }
        Pozycja[] lista;
        int ile=0;
        double razem=0;
        public DostWindow()
        {
            InitializeComponent();
            lista = new Pozycja[20];
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ViewBase vb = new ViewBase(1);
            vb.Order.Content = "Dodaj do dostawy";
            vb.AddItemCallback = new AddItemDelegate(this.AddItemCallbackFn);
            vb.ShowDialog();

        }
        private void AddItemCallbackFn(Lek item)
        {

            AddToList(item);

        }
        private void AddToList(Lek lek)
        {
            lista[ile] = new Pozycja();
            lista[ile].lekitem = lek;
            lista[ile].ile = 1;
            ile++;
            Refresh();
        }
        private void Refresh()
        {
            Postac pos;
            Leki.Items.Clear();
            razem = 0;
            for (int i = 0; i <= ile - 1; i++)
            {
                razem += lista[i].lekitem.Cena * lista[i].ile;
                pos = (from p in con.Postacs where p.PostacID == lista[i].lekitem.LekID select p).FirstOrDefault();
                if (lista[i].ile == 1)
                Leki.Items.Add((i+1).ToString()+" "+lista[i].lekitem.Nazwa + " " + lista[i].lekitem.Dawka + " MG (" + lista[i].lekitem.Opakowanie.ToString() + ") " + pos.Nazwa + "  " + lista[i].lekitem.Cena.ToString() + " zł");
                else
                    Leki.Items.Add((i + 1).ToString() + " " + lista[i].lekitem.Nazwa + " " + lista[i].lekitem.Dawka + " MG (" + lista[i].lekitem.Opakowanie.ToString() + ") " + pos.Nazwa + " x  " + lista[i].ile.ToString() + " "+ (lista[i].lekitem.Cena * lista[i].ile).ToString() + " zł");

            }
            Ilosc.Content = ile.ToString();
            Razem.Content = razem.ToString()+" zł";
            po_ile.Text = "";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Usun_Click(object sender, RoutedEventArgs e)
        {
            if (Leki.SelectedIndex != -1)
            {
                if (lista[Leki.SelectedIndex].ile == 1)
                {
                    for (int i = Leki.SelectedIndex; i <= ile - 1; i++)
                    {
                        lista[i] = lista[i + 1];
                    }
                    ile--;
                }
                else
                    lista[Leki.SelectedIndex].ile--;
                Refresh();
            }
            else
                MessageBox.Show("Nie wybrano zadnej pozycji!");
        }

        private void Leki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Leki.SelectedIndex != -1)
            {
                po_ile.Text = lista[Leki.SelectedIndex].ile.ToString();
            }
        }

        private void po_ile_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            if (Leki.SelectedIndex != -1)
            {
                if (Convert.ToInt32(po_ile.Text) > 0)
                {
                    lista[Leki.SelectedIndex].ile = Convert.ToInt32(po_ile.Text);
                    Refresh();
                }
                else
                    MessageBox.Show("Ilość musi być więcej niż 0!");
            }
        }

        private void Realizuj_Click(object sender, RoutedEventArgs e)
        {
            using (AptekaDataDataContext con = new AptekaDataDataContext())
            {

                //try
               // {
                    Dostawa dost = new Dostawa();
                    Lek lektmp = new Lek();
                    dost.Data_zam = DateTime.Today;
                    dost.Data_realizacji = DateTime.Today;
                    con.Dostawas.InsertOnSubmit(dost);
                    con.SubmitChanges();
                    for (int i = 0; i <= ile - 1; i++)
                    {
                        lektmp = (from l in con.Leks where l.LekID == lista[i].lekitem.LekID select l).FirstOrDefault();
                        lektmp.Ilosc = lektmp.Ilosc + lista[i].ile;
                        con.SubmitChanges();
                        Dost_ilosc di = new Dost_ilosc();
                        di.LekID = lista[i].lekitem.LekID;
                        di.DostawaID = dost.DostawaID;
                        di.Ilosc = lista[i].ile;
                        con.Dost_iloscs.InsertOnSubmit(di);
                        con.SubmitChanges();
                    }
                    MessageBox.Show("Dostawa została zrealizowana!");
                    DialogResult = true;
               /* }
                catch
                {
                    MessageBox.Show("Wystapił bląd pod czas łaczenia sie z bazą!");
                    DialogResult = false;
                }*/
            }
        }

    }
}
