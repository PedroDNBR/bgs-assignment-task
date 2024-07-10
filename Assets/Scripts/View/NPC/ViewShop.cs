using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
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

                // Creates the UI template using the items value
                item.Key.SetItemForShopInUI(
                    newUIItemTemplate,
                    item.Value,
                    "Buy",
                    () => shop.PurchaseItem(item.Key, playerInventory)
                );

                // Activate template
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