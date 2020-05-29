using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;


namespace TrainSystem
{
    public class TrainDessertManager : MonoBehaviour
    {
        private InventoryData inventoryData;
        public GameObject itemSlotGrid;
        public GameObject itemSlot;
        public GameObject cloneSlot;

        private void Awake()
        {
            inventoryData = GameObject.Find("GameData").GetComponent<InventoryData>();
        }
        public void Initialize()
        {
            this.gameObject.SetActive(true);
            DeetoryDessertPanel();
            RefreshDessertPanel();
        }

        public void RefreshDessertPanel()
        {
            foreach(Item item in inventoryData.GetDessertInventory())
            {
                cloneSlot = Instantiate(itemSlot, itemSlotGrid.transform);
                cloneSlot.GetComponent<TrainSystem.ItemSlot>().thisItem = item;
            }
            foreach(Item item in inventoryData.GetIngredientInventory())
            {
                cloneSlot = Instantiate(itemSlot, itemSlotGrid.transform);
                cloneSlot.GetComponent<TrainSystem.ItemSlot>().thisItem = item;
            }
        }

        public void DeetoryDessertPanel()
        {
            foreach (Transform slot in itemSlotGrid.transform)
            {
                Destroy(slot.gameObject);
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                DeetoryDessertPanel();
                this.gameObject.SetActive(false);
            }
        }
    }
}
