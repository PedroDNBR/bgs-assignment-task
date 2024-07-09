using JetBrains.Annotations;
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
    }

    public class ShopItemList
    {
        public IBaseItem item;
        public int quantity;
    }
}
