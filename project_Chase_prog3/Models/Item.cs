using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Chase_prog3.Models
{
    public class Item
    {
        private string itemName; 
        private int availQty; 
        private int minQty; 
        private int isleNum; 
        private string supplier; 
        private Category category; 
        private string identifier;

        //defaul constructor
        public Item()
        {

        }
        //constructor
        public Item(string Name, int availQty, int minQty, Category category, int isleNum, string itemSupplier)
        {
            itemName = Name;
            AvailableQty = availQty;
            MinQty = minQty;
            ICategory = category;
            IsleNum = isleNum;
            supplier = itemSupplier;

            Id = itemName;
        }

        
        public enum Category
        {
            PANTRY,
            DAIRY,
            DRINKS,
            FROZEN_FOOD,
            FRUIT_AND_VEGETABLES,
            BAKERY,
            CLEANING_SUPPLIES,
            OTHER
        }

        #region class properties
       
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public int AvailableQty
        {
            get { return availQty; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("cannot be negative", "AvailableQty"); 
                availQty = value;
            }
        }

        public int MinQty
        {
            get { return minQty; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("cannot be negative", "MinimumQty"); 
                minQty = value;
            }
        }

        public int IsleNum
        {
            get { return isleNum; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("cannot be negative", "IsleNumber"); 
                isleNum = value;
            }
        }

        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        public Category ICategory
        {
            get { return category; }
            set
            {
                
                category = value;
            }
        }


        // used to compare items will use the items name
        public string Id 
        {
            get { return identifier; }
            private set
            {
                
                identifier = itemName;
            }
        }

        #endregion


        public override string ToString()
        {
            return string.Format("   Item Name: {0}\n   Item Category: {1}\n   Supplier: {2}\n   Available Quantity: {3}\n   " +
                "Minimum Quantity: {4}\n   Isle Number: {5}\n", itemName, category, supplier, availQty, minQty, isleNum);
        }

        /// <summary>
        /// copied from Arefs' code used to write and save to files
        /// </summary>
        public string CSVData
        {
            get { return string.Format($"{ItemName},{AvailableQty},{MinQty},{Supplier},{IsleNum},{ICategory}"); }

            set
            {

                //string comma separated and set the fields of the visitor
                string[] Data = value.Split(',');
                try
                {
                    ItemName = Data[0];
                    Id = Data[0];
                    AvailableQty = int.Parse(Data[1]);
                    MinQty = int.Parse(Data[2]);
                    Supplier = Data[3];
                    IsleNum = int.Parse(Data[4]);
                    ICategory = Data[5] == Category.BAKERY.ToString() ? Category.BAKERY :
                                Data[5] == Category.CLEANING_SUPPLIES.ToString() ? Category.CLEANING_SUPPLIES :
                                Data[5] == Category.DAIRY.ToString() ? Category.DAIRY :
                                Data[5] == Category.DRINKS.ToString() ? Category.DRINKS :
                                Data[5] == Category.FRUIT_AND_VEGETABLES.ToString() ? Category.FRUIT_AND_VEGETABLES :
                                Data[5] == Category.FROZEN_FOOD.ToString() ? Category.FROZEN_FOOD :
                                Data[5] == Category.PANTRY.ToString() ? Category.PANTRY : Category.OTHER;
                }
                catch
                {
                    throw new ArgumentException("Data Property value not valid", "CVSData");
                }
            }
        }

    }


}

  
