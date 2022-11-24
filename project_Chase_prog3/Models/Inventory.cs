using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_Chase_prog3.Models
{
    public class Inventory
    {
        private List<Item> _items;
        // constructor
        public Inventory()
        {
            _items = new List<Item>();
        }
        /// <summary>
        /// adds to the item list
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// removes item from list based on id
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            foreach (Item i in _items)
            {
                if (item.Id == i.Id)
                {
                    _items.Remove(i);
                    break;
                }
            }
        }

        /// <summary>
        /// removes an item and adds a new one based on id 
        /// </summary>
        /// <param name="item"></param>
        public void UpdateItem(Item item)
        {
            foreach (Item i in _items)
            {
                if (item.ItemName == i.ItemName)
                {
                    _items.Remove(i);
                    _items.Add(item);
                }
            }
        }

        /// <summary>
        /// filters by available quantity > minimum quantity
        /// </summary>
        /// <returns>items list </returns>
        public List<Item> Shopping()
        {
            List<Item> shopping = new List<Item>();
            foreach (Item i in _items)
            {
                if (i.AvailableQty <= i.MinQty)
                    shopping.Add(i);
            }

            return shopping;
        }

        
        //used to return items in  items list
        public List<Item> GetItems()
        {
            return _items;
        }

    }
}
