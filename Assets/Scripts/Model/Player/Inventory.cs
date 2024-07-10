using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGS
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] BaseItem head;
        [SerializeField] BaseItem torso;
        [SerializeField] BaseItem pants;
        [SerializeField] BaseItem shoes;

        public BaseItem Head { get => head; set => head = value; }
        public BaseItem Torso { get => torso; set => torso = value; }
        public BaseItem Pants { get => pants; set => pants = value; }
        public BaseItem Shoes { get => shoes; set => shoes = value; }

        public int gold = 10;

        Dictionary<IBaseItem, int> itemsInInventory = new Dictionary<IBaseItem, int>();
        public Dictionary<IBaseItem, int> ItemsInInventory { get => itemsInInventory; }

        public event Action ToggleUIInventory;
        public event Action HideUIInventory;
        public event Action HideUISellInventory;
        public event Action EquippedUIItem;

        public event Action<IBaseItem, bool> EquipmentEquipped;

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

        public void SellItem(IBaseItem item, Shop shop, AudioSource sellAudio)
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
                sellAudio.Play();
            }
        }

        public void Equip(IBaseItem item)
        {
            if (ItemsInInventory.ContainsKey(item))
            {
                ItemsInInventory[item]--;
                if (ItemsInInventory[item] <= 0)
                {
                    ItemsInInventory.Remove(item);
                }
                switch (item.Type)
                {
                    case ItemType.Hat:
                        if(Head != null)
                        {
                            AddItemToInventory(Head);
                            Head = null;
                        }
                        Head = item as BaseItem;
                        break;
                    case ItemType.Shirt:
                        if (Torso != null)
                        {
                            AddItemToInventory(Torso);
                            Torso = null;
                        }
                        Torso = item as BaseItem;
                        break;
                    case ItemType.Pants:
                        if (Pants != null)
                        {
                            AddItemToInventory(Pants);
                            Pants = null;
                        }
                        Pants = item as BaseItem;
                        break;
                    case ItemType.Shoes:
                        if (Shoes != null)
                        {
                            AddItemToInventory(Shoes);
                            Shoes = null;
                        }
                        Shoes = item as BaseItem;
                        break;
                }
                UpdateEquipmentUI();
                if (EquipmentEquipped != null) EquipmentEquipped(item, true);
            }
        }

        public void UnequipItem(IBaseItem item)
        {
            switch (item.Type)
            {
                case ItemType.Hat:
                    Head = null;
                    break;
                case ItemType.Shirt:
                    Torso = null;
                    break;
                case ItemType.Pants:
                    Pants = null;
                    break;
                case ItemType.Shoes:
                    Shoes = null;
                    break;
            }
            UpdateEquipmentUI();
            AddItemToInventory(item);
            if (EquipmentEquipped != null) EquipmentEquipped(item, false);
        }


        void UpdateEquipmentUI()
        {
            if (EquippedUIItem != null) EquippedUIItem();
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
