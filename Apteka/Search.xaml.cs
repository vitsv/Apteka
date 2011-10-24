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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        { 
            AptekaDataDataContext con = new AptekaDataDataContext();
            ViewBase viewbase = new ViewBase();
            List<Lek> lek = (from l in con.Leks where l.Nazwa.StartsWith(textBox1.Text) || l.M_nazwa.StartsWith(textBox2.Text) select l).ToList();
            viewbase.LekGird.ItemsSource = lek;
            viewbase.search = 1;
            viewbase.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void checkBox3_Checked(object sender, RoutedEventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
