using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DessertSystem;
using Newtonsoft.Json;

namespace InventorySystem
{
    public class InventoryData : MonoBehaviour
    {
        static InventoryData instance;

        private static List<Item> ingredientInventory;
        private static List<Item> dessertInventory;
        private static List<Item> otherInventory;

        private static CookBook myCookBook; 

        public void Awake()
        {
            if (instance != null)
                Destroy(this);
            instance = this;
            ingredientInventory = new List<Item>();    
            dessertInventory = new List<Item>();    
            otherInventory = new List<Item>();
            myCookBook = new CookBook();

            AddItem(new Item { itemName = Item.ItemName.雞蛋 , quantity = 2 });
            AddItem(new Item { itemName = Item.ItemName.巧克力, quantity = 1 });
            AddItem(new Item { itemName = Item.ItemName.牛奶, quantity = 2 });

            List<Item> recipe = new List<Item>();

            Recipe cake ;
            myCookBook.GetCookBook().Add(
                Item.ItemName.巧克力蛋糕, cake = new Recipe(
                    new Item { itemName = Item.ItemName.雞蛋, quantity = 1 },
                    new Item { itemName = Item.ItemName.巧克力, quantity = 1 },
                    new Item { itemName = Item.ItemName.牛奶, quantity = 1 }, recipe)
            );
        }

        public void AddItem(Item item)
        {
            bool itemExist = false;

            foreach (Item iL in GetInventory(item))
            {
                if (item.itemName == iL.itemName)
                {
                    itemExist = true;
                    iL.quantity += item.quantity;
                }
            }

            if (!itemExist)
            {
                GetInventory(item).Add(item);
            }
        }

        public void RemoveItem(Item item)
        {
            if (FindItem(item) != null && FindItem(item).quantity >= item.quantity)
            {
                FindItem(item).quantity -= item.quantity;
                if(FindItem(item).quantity == 0)
                {
                    GetInventory(item).Remove(FindItem(item));
                }
            }
        }

        public List<Item> GetInventory(Item item)
        {
            switch (Item.ItemType(item))
            {
                case 0:
                    return ingredientInventory;
                case 1:
                    return dessertInventory;
                case 2:
                    return otherInventory;
                default:
                    Debug.Log("can not find inventory");
                    return null;
            }
        }

        public Item FindItem(Item item)
        {
            foreach (Item iL in GetInventory(item))
            {
                if (iL.itemName == item.itemName)
                    return iL;
            }
            Debug.Log("cant find item");
            return null;
        }

        public List<Item> GetIngredientInventory()
        {
            return ingredientInventory;
        }

        public List<Item> GetDessertInventory()
        {
            return dessertInventory;
        }

        public List<Item> GetOtherInventory()
        {
            return otherInventory;
        }

        public Dictionary<Item.ItemName, Recipe> GetCookBook()
        {
            return myCookBook.GetCookBook();
        }
    }
}
