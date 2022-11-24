using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using project_Chase_prog3.Models;

namespace project_Chase_prog3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     
    public partial class MainWindow : Window
    {
        public Inventory inventory;
        //private Item UpdatingItem;
        private List<Item.Category> categories = Enum.GetValues(typeof(Item.Category)).Cast<Item.Category>().ToList();

        private string saveLocation = string.Empty;
        private bool saved = false;
        public MainWindow()
        {
            InitializeComponent();
            inventory = new Inventory();
            List<Item> items = inventory.GetItems();

            lbItems.ItemsSource = inventory.GetItems();
            //comboCategory.ItemsSource = categories;
            //UpdatingItem = null;
        }

        /// <summary>
        /// submit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Button_Submit(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string name = txtName.Text;
        //        int isleNum = int.Parse(txtIsleNum.Text);
        //        string supplier = txtSupplier.Text;
        //        int availableQunatity = int.Parse(txtAvailableQty.Text);
        //        int minQty = int.Parse(txtMinQty.Text);
        //        string stringCategory = comboCategory.SelectedItem.ToString();
        //        Item.Category category = Item.Category.OTHER;
        //        foreach (Item.Category cat in categories)
        //        {
        //            if (stringCategory == cat.ToString())
        //            {
        //                category = cat;
        //            }
        //        }

        //        if (UpdatingItem != null)
        //        {
        //            UpdatingItem.ItemName = name;
        //            UpdatingItem.IsleNum = isleNum;
        //            UpdatingItem.Supplier = supplier;
        //            UpdatingItem.AvailableQty = availableQunatity;
        //            UpdatingItem.MinQty = minQty;
        //            UpdatingItem.ICategory = category;

        //            UpdatingItem = null;
        //        }
        //        else
        //        {
        //            Item item = new Item(name, availableQunatity, minQty, category, isleNum, supplier);
        //            inventory.AddItem(item);
        //        }

        //        lbItems.Items.Refresh();
        //        txtName.Clear();
        //        txtIsleNum.Clear();
        //        txtSupplier.Clear();
        //        txtAvailableQty.Clear();
        //        txtMinQty.Clear();
        //        comboCategory.SelectedIndex = -1;
        //        saved = false;
        //    }
        //    catch(Exception ex)
        //    {
        //        //todo fix USE STRING BUILDER 
        //        MessageBox.Show("something went wrong"+ex.Message);
        //    }
           
        //}


        #region Right Click Options
        private void MenuItem_Delete(object sender, RoutedEventArgs e)
        {

            Item i = (Item)lbItems.SelectedItem;
            inventory.RemoveItem(i);
            lbItems.Items.Refresh();


        }

        private void MenuItem_Update(object sender, RoutedEventArgs e)
        {
            List<Item> shoppingList = inventory.GetItems();
            Item i = (Item)lbItems.SelectedItem;
            AddItems window = new AddItems(shoppingList, i);

            window.Show();


            //txtName.Text = i.ItemName;
            //txtIsleNum.Text = i.IsleNum.ToString();
            //txtSupplier.Text = i.Supplier;
            //txtAvailableQty.Text = i.AvailableQty.ToString();
            //txtMinQty.Text = i.MinQty.ToString();
            //comboCategory.SelectedItem = i.ICategory;

            //UpdatingItem = i;
        }
        #endregion

        #region File Methods
        private void SaveLogic()
        {
            if (string.IsNullOrEmpty(saveLocation))
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CVS Files|*.csv";//"Description|*.xyz|file2|*.file2";
                if (save.ShowDialog() == true)
                {
                    saveLocation = save.FileName;
                }
                else
                    return;
            }
            WriteDataToFile();

        }

        private void WriteDataToFile()
        {
            if (!saved)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Item items in inventory.GetItems())
                        sb.AppendLine(items.CSVData);

                    File.WriteAllText(saveLocation, sb.ToString());
                    saved = true;
                }
                catch (Exception)
                {

                    //todo add ok etc
                    MessageBox.Show("Error Reading frmo file");
                }
            }
        }

        private bool ChecktoOpen()
        {
            if (saved)
                return true;

            if (string.IsNullOrEmpty(saveLocation) && inventory.GetItems().Count == 0)
                return true;


            //Data is not saved
            MessageBoxResult result =
            MessageBox.Show("Do you want to save changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);


            if (result == MessageBoxResult.No)
                return true;

            if (result == MessageBoxResult.Cancel)
                return false;

            if (result == MessageBoxResult.Yes)
                SaveLogic();

            return saved; //if the user saved it mean it ok to open a file and if the user cancels then saved will be false

        }

        private void ReadFromFile()
        {
            try
            {
                string[] allValues = File.ReadAllLines(saveLocation);
                //create visitor objects and add them to list
                foreach (string visitorInfo in allValues)
                {
                    //visitors.Add(new Visitor() { CSVData = visitorInfo });
                    Item temp = new Item();
                    temp.CSVData = visitorInfo;
                    inventory.AddItem(temp);
                }
            }
            catch (Exception)
            {
                //todo add ok etc
                MessageBox.Show("Error Reading frmo file");
            }
        }

        #endregion

        #region Other Buttons
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            SaveLogic();
        }

        private void Button_Open(object sender, RoutedEventArgs e)
        {
            if (ChecktoOpen())
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "CVS Files|*.csv";
                if (open.ShowDialog() == true)
                {
                    //save location
                    saveLocation = open.FileName;
                    //clear current list
                    inventory.GetItems().Clear();
                    //read from file
                    ReadFromFile();
                    //update UI
                    lbItems.Items.Refresh();
                    saved = true;
                }
            }
        }

        private void Button_Shopping_Window(object sender, RoutedEventArgs e)
        {
            List<Item> shoppingList = inventory.Shopping(); 
            Shopping window = new Shopping(shoppingList);

            window.Show();

        }

        private void Button_General(object sender, RoutedEventArgs e)
        {
            List<Item> shoppingList = inventory.GetItems();
            Shopping window = new Shopping(shoppingList);

            window.Show();
        }

        //public methods for later use
        public void SaveMethod()
        {
            saved = false;
        }

        public void RefreshMethod()
        {
            lbItems.Items.Refresh();
        }

        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        /// <summary>
        ///  for adding to the items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            List<Item> shoppingList = inventory.GetItems();
            Item iteminput = null;
            AddItems window = new AddItems(shoppingList, iteminput);

            window.Show();
        }
        #endregion



    }
}
