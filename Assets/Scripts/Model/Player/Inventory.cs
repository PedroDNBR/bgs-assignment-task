using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGS
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] IBaseItem Head;
        [SerializeField] IBaseItem Torso;
        [SerializeField] IBaseItem Pants;
        [SerializeField] IBaseItem Shoes;

        public int gold = 10;

        Dictionary<IBaseItem, int> itemsInInventory = new Dictionary<IBaseItem, int>();
        public Dictionary<IBaseItem, int> ItemsInInventory { get => itemsInInventory; }

        public event Action ToggleUIInventory;
        public event Action HideUIInventory;
        public event Action HideUISellInventory;
        
        public event Action<Shop> ToggleUISellInventory;

        public void AddItemToInventory(IBaseItem item)
        {
            if(ItemsInInventory.ContainsKey(item))
            {
                ItemsInInventory[item]++;
            }
            else
            {
                ItemsInInventory.Add(item, 1);
            }
        }

        public void SellItem(IBaseItem item, Shop shop)
        {
            if (ItemsInInventory.ContainsKey(item))
            {
                if (ItemsInInventory[item] <= 1)
                {
                    ItemsInInventory.Remove(item);
                }
                else
                {
                    ItemsInInventory[item]--;
                }
                gold += item.Price;
                shop.AddBackToStock(item, this);
            }
        }

        public void ToggleInventory()
        {
            if (ToggleUIInventory != null) ToggleUIInventory();
        }

        public void HideInventoy()
        {
            if (HideUIInventory != null) HideUIInventory();
        }

        public void ToggleSellInventory(Shop shop)
        {
            if (ToggleUISellInventory != null) ToggleUISellInventory(shop);
        }

        public void HideSellInventory()
        {
            if (HideUISellInventory != null) HideUISellInventory();
        }

    }
}
