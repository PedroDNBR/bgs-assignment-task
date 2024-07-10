using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGS
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] List<ShopItemList> startingItemsList = new List<ShopItemList>();

        // Dictionary<Item, Quantity>
        Dictionary<IBaseItem, int> itemsInShop = new Dictionary<IBaseItem, int>();

        public Dictionary<IBaseItem, int> ItemsInShop { get => itemsInShop; }

        public event Action<Inventory> itemPurchased;
        public event Action<Shop> purchasedItemWentToInventory;

        private void Start()
        {
            for (int i = 0; i < startingItemsList.Count; i++)
            {
                itemsInShop.Add(
                    startingItemsList[i].item, 
                    startingItemsList[i].quantity
                );
            }
        }

        public void PurchaseItem(IBaseItem item, Inventory playerInventory, AudioSource purchaseAudio)
        {
            if(playerInventory.gold >= item.Price && itemsInShop[item] > 0)
            {
                itemsInShop[item]--;
                playerInventory.gold -= item.Price;
                playerInventory.AddItemToInventory(item);
                purchaseAudio.Play();

                if (itemPurchased != null) itemPurchased(playerInventory);
                if (purchasedItemWentToInventory != null) purchasedItemWentToInventory(this);
            }
        }

        public void AddBackToStock(IBaseItem item, Inventory playerInventory)
        {
            if(ItemsInShop.ContainsKey(item))
            {
                ItemsInShop[item]++;
                if (itemPurchased != null) itemPurchased(playerInventory);
                if (purchasedItemWentToInventory != null) purchasedItemWentToInventory(this);
            }
        }
    }

    [Serializable]
    public class ShopItemList
    {
        [SerializeField] public BaseItem item;
        [SerializeField] public int quantity;
    }
}
