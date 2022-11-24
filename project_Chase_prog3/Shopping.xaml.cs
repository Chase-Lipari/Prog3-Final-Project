using project_Chase_prog3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace project_Chase_prog3
{
    /// <summary>
    /// Interaction logic for Shopping.xaml
    /// </summary>
    public partial class Shopping : Window
    {
        public Shopping(List<Item> items)
        {
            
            InitializeComponent();

            gridTable.ItemsSource = items;

        }
    }
}
