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
    /// Interaction logic for AddItems.xaml
    /// </summary>
    public partial class AddItems : Window
    {
        private Item UpdatingItem;

        private List<Item.Category> categories = Enum.GetValues(typeof(Item.Category)).Cast<Item.Category>().ToList();
        public AddItems(List<Item> items, Item i)
        {
            InitializeComponent();

            comboCategory.ItemsSource = categories;
            UpdatingItem = i;
            if (UpdatingItem != null)
            {
                //Item i = (Item)lbItems.SelectedItem;
                txtName.Text = i.ItemName;
                txtIsleNum.Text = i.IsleNum.ToString();
                txtSupplier.Text = i.Supplier;
                txtAvailableQty.Text = i.AvailableQty.ToString();
                txtMinQty.Text = i.MinQty.ToString();
                comboCategory.SelectedItem = i.ICategory;

                UpdatingItem = i;
            }

        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                int isleNum = int.Parse(txtIsleNum.Text);
                string supplier = txtSupplier.Text;
                int availableQunatity = int.Parse(txtAvailableQty.Text);
                int minQty = int.Parse(txtMinQty.Text);
                string stringCategory = comboCategory.SelectedItem.ToString();
                Item.Category category = Item.Category.OTHER;
                foreach (Item.Category cat in categories)
                {
                    if (stringCategory == cat.ToString())
                    {
                        category = cat;
                    }
                }
                MainWindow currentWin = (MainWindow)Application.Current.MainWindow;

                if (UpdatingItem != null)
                {
                    UpdatingItem.ItemName = name;
                    UpdatingItem.IsleNum = isleNum;
                    UpdatingItem.Supplier = supplier;
                    UpdatingItem.AvailableQty = availableQunatity;
                    UpdatingItem.MinQty = minQty;
                    UpdatingItem.ICategory = category;

                    UpdatingItem = null;
                }
                else
                {
                    Item item = new Item(name, availableQunatity, minQty, category, isleNum, supplier);
                    currentWin.AddItem(item);
                }

                currentWin.RefreshMethod();
                txtName.Clear();
                txtIsleNum.Clear();
                txtSupplier.Clear();
                txtAvailableQty.Clear();
                txtMinQty.Clear();
                comboCategory.SelectedIndex = -1;
                currentWin.SaveMethod();

                Close();
            }
            catch (Exception ex)
            {
                //todo fix USE STRING BUILDER 
                MessageBox.Show("something went wrong" + ex.Message);
            }

        }
    }
}
