using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS
{
    public class ViewShop : MonoBehaviour
    {
        Shop shop;

        [Header("UI elements")]
        public Transform UIShop;
        public GameObject UIItemTemplate;
        public VerticalLayoutGroup UIItemGrid;

        private void Start()
        {
            shop = GetComponent<Shop>();
            shop.itemPurchased += OpenShop;
        }

        public void FillShopWithItems(Inventory playerInventory)
        {
            foreach(KeyValuePair<IBaseItem, int> item in shop.ItemsInShop)
            {
                GameObject newUIItemTemplate = Instantiate(UIItemTemplate, UIItemGrid.transform);
                // Prints the item name
                newUIItemTemplate.
                    GetComponentsInChildren<TextMeshProUGUI>()[0].
                    text = $"{item.Key.ItemName}";

                // Prints the item Quantity
                newUIItemTemplate.
                    GetComponentsInChildren<TextMeshProUGUI>()[1].
                    text = $"x{item.Value}";

                // Prints the item price
                newUIItemTemplate.
                    GetComponentsInChildren<TextMeshProUGUI>()[2].
                    text = $"{item.Key.Price} Gold";

                // Button event to purchase the item
                newUIItemTemplate.
                    GetComponentInChildren<Button>().
                    onClick.AddListener(() => shop.PurchaseItem(item.Key, playerInventory));

                // TODO - Update Icon

                newUIItemTemplate.SetActive(true);
            }
        }

        void ClearShop()
        {
            foreach (Transform child in UIItemGrid.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void OpenShop(Inventory playerInventory)
        {
            ClearShop();
            FillShopWithItems(playerInventory);
            ShowShop();
        }

        public void ShowShop()
        {
            if (UIShop == null) return;
            UIShop.gameObject.SetActive(true);
        }

        public void HideShop()
        {
            if (UIShop == null) return;
            UIShop.gameObject.SetActive(false);
        }
    }
}