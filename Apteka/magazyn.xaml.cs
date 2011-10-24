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
using System.Net;
using System.Net.Sockets;
using System.IO;
using komunikaty;
using System.ComponentModel;
using System.Data;


namespace Apteka
{
    /// <summary>
    /// Interaction logic for magazyn.xaml
    /// </summary>
    public partial class magazyn : Window
    {
        AptekaDataDataContext con = new AptekaDataDataContext();
        private int status = 0;
        public magazyn()
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Polaczenie("czy_jest");
        }
        private TcpClient klient = null;
        private bool czypolaczono = false;
        private BinaryReader r = null;
        private BinaryWriter w = null;

        private void Polaczenie(string mode)
        {
            try
            {
                klient = new TcpClient();
                log.Content = "Próbuje się połączyć";
                klient.Connect(IPAddress.Parse(txtIP.Text), int.Parse(txtPort.Text));
                log.Content = "Połączenie nawiązane...";
                NetworkStream stream = klient.GetStream();
                w = new BinaryWriter(stream);
                r = new BinaryReader(stream);
                string odp;
                if (mode == "czy_ok")
                {
                        MessageBox.Show("Jest połączenie");
                        w.Write(KomunikatyKlienta.Zadaj);

                        if (r.ReadString() == KomunikatySerwera.OK)
                            w.Write("fsdf");
                        klient.Close();
                }
                if (mode == "czy_jest")
                {
                    Lek selected = LekGird.SelectedItem as Lek;
                    if (selected == null)
                        MessageBox.Show("Nie wybrano żadnej pozycji!");
                    else
                    {
                        w.Write("###__CZY_JEST:"+selected.LekID);
                     //   odp = r.ReadString();
                      //  if (Convert.ToInt32(odp) > 0)
                       //     MessageBox.Show("Magazyn posiada wybrany lek w ilości: "+odp+" .");
                      //  else
                     //       MessageBox.Show("Magazyn posiada wybrany lek w ilości: " + odp + " .");
                    }
                    
                }
                if (mode == "zamow")
                {
                    Lek selected = LekGird.SelectedItem as Lek;
                    if (selected == null)
                        MessageBox.Show("Nie wybrano żadnej pozycji!");
                    else
                    {
                        w.Write("###__CZY_JEST:" + selected.LekID);
                        odp = r.ReadString();
                        if (Convert.ToInt32(odp) >= Convert.ToInt32(ile.Text))
                        {
                            w.Write("###__ZAMOW:" + selected.LekID + ":" + ile.Text);
                            MessageBox.Show("Zamówienie zostało przyjęte");
                        }   
                        else
                            MessageBox.Show("Nie wystarczajaca ilosc, magazyn posiada wybrany lek w ilości: " + odp + " .");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Brak łączności z serverem!");
            }
        }

        /*private void Odbieranie_DoWork()
        {
            string tekst;
            while ((tekst = r.ReadString()) != KomunikatySerwera.Rozlacz)
            {
                //wyswietl(txtOtrzymywane, "===== Rozmуwca =====\n" + tekst + '\n');
            }
            //cmdWyslij.Enabled = false;
          //  log.Content =  "Rozlaczono";
           // plc.Content = "Polacz";
            status = 0;
            czypolaczono = false;
            if (klient != null) klient.Close();
        }*/

        private void cmdWyslij()
        {
            string tekst = "cos";//txtWysylane.Text;
           // if (tekst == "") { txtWysylane.Focus(); return; }
            if (tekst[tekst.Length - 1] == '\n')
                tekst = tekst.TrimEnd('\n');
            w.Write(tekst);
            //wyswietl(txtOtrzymywane, "===== Ja =====\n" + tekst + '\n');
           // txtWysylane.Text = "";
        }

       /* private void txtWysylane_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmdWyslij.Enabled && e.KeyChar == (char)13) cmdWyslij_Click(sender, e);
        }*/

        private void cmdPolacz()
        {
                
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            if (czypolaczono)
            {
                w.Write(KomunikatyKlienta.Rozlacz);
                klient.Close();
                czypolaczono = false;
            }
            //Polaczenie.CancelAsync();
            //Odbieranie.CancelAsync();
        }

        private void plc_Click(object sender, RoutedEventArgs e)
        {
            Polaczenie("czy_ok");
        }
        private void Polaczenie_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void Odbieranie_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            if (ile.Text != "")
                Polaczenie("zamow");
            else
                MessageBox.Show("Ilosc nie moze byc pusta!");
        }

    }
}
