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

        public Shop Shop { get => shop; }

        [Header("UI elements")]
        public Transform UIShop;
        public GameObject UIItemTemplate;
        public VerticalLayoutGroup UIItemGrid;

        [Header("UI outside Canvas")]
        public SpriteRenderer interactionSprite;


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
                item.Key.SetItemNameInUI(newUIItemTemplate);

                // Prints the item Quantity
                item.Key.SetItemQuantityInUI(newUIItemTemplate, item.Value);
                
                // Prints the item price
                item.Key.SetItemPriceInUI(newUIItemTemplate);

                // Button event to purchase the item
                item.Key.SetItemButtonAction(
                    newUIItemTemplate,
                    "Buy",
                    () => shop.PurchaseItem(item.Key, playerInventory)
                );

                // SetIcon
                item.Key.SetItemIconInUI(newUIItemTemplate);

                // 
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

        public void ToggleShop(Inventory playerInventory)
        {
            if(UIShop.gameObject.activeSelf)
            {
                HideShop();
            }
            else
            {
                OpenShop(playerInventory);
            }
        }

        public void OpenShop(Inventory playerInventory)
        {
            ClearShop();
            FillShopWithItems(playerInventory);
            ShowShop();
        }

        public void ShowInteractionIcon()
        {
            interactionSprite.gameObject.SetActive(true);
        }

        public void HideInteractionIcon()
        {
            interactionSprite.gameObject.SetActive(false);
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