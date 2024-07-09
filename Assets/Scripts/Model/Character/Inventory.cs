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

        Dictionary<IBaseItem, int> itemsInInventory = new Dictionary<IBaseItem, int>();
        public Dictionary<IBaseItem, int> ItemsInInventory { get => itemsInInventory; }

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
    }
}
