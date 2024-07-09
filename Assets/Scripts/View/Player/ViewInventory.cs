using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS
{
    public class ViewInventory : MonoBehaviour
    {
        Inventory inventory;

        [Header("UI elements")]
        public Transform UIInventory;
        public GameObject UIItemTemplate;
        public VerticalLayoutGroup UIItemGrid;
        public TextMeshProUGUI UIGoldText;

        [Header("UI Sell Menu elements")]
        public Transform UISellInventory;
        public GameObject UISellItemTemplate;
        public VerticalLayoutGroup UISellItemGrid;
        public TextMeshProUGUI UIGoldSellText;

        private void Start()
        {
            inventory = GetComponent<Inventory>();
            inventory.ToggleUIInventory += ToggleInventory;
            inventory.HideUIInventory += HideInventory;
            inventory.ToggleUISellInventory += ToggleSellInventory;
            inventory.HideUISellInventory += HideSellInventory;
        }

        public void FillInventoryWithItems()
        {
            foreach (KeyValuePair<IBaseItem, int> item in inventory.ItemsInInventory)
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


                // TODO - Update Icon
                newUIItemTemplate.SetActive(true);
            }
        }

        public void FillSellInventoryWithItems(Shop shop)
        {
            foreach (KeyValuePair<IBaseItem, int> item in inventory.ItemsInInventory)
            {
                GameObject newUIItemTemplate = Instantiate(UISellItemTemplate, UISellItemGrid.transform);
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
                    onClick.AddListener(() => inventory.SellItem(item.Key, shop));

                // TODO - Update Icon

                newUIItemTemplate.SetActive(true);
            }
        }

        void UpdateGoldInInventory()
        {
            UIGoldText.text = $"Gold: {inventory.gold}";
        }

        void UpdateGoldInSellInventory()
        {
            UIGoldSellText.text = $"Gold: {inventory.gold}";
        }

        void ToggleInventory()
        {
            if (UIInventory == null) return;
            if(UIInventory.gameObject.activeSelf)
            {
                HideInventory();
            }
            else
            {
                OpenInventory();
            }
        }

        void ClearInventory()
        {
            foreach (Transform child in UIItemGrid.transform)
            {
                Destroy(child.gameObject);
            }
        }

        void ClearSellInventory()
        {
            foreach (Transform child in UISellItemGrid.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void OpenInventory()
        {
            ClearInventory();
            FillInventoryWithItems();
            UpdateGoldInInventory();
            ShowInventory();
        }

        public void OpenSellInventory(Shop shop)
        {
            ClearSellInventory();
            FillSellInventoryWithItems(shop);
            UpdateGoldInSellInventory();
            ShowSellInventory();
        }

        public void ShowInventory()
        {
            if (UIInventory == null) return;
            UIInventory.gameObject.SetActive(true);
        }

        public void HideInventory()
        {
            if (UIInventory == null) return;
            UIInventory.gameObject.SetActive(false);
        }

        public void ShowSellInventory()
        {
            if (UISellInventory == null) return;
            UISellInventory.gameObject.SetActive(true);
        }

        public void HideSellInventory()
        {
            if (UISellInventory == null) return;
            UISellInventory.gameObject.SetActive(false);
        }

        void ToggleSellInventory(Shop shop)
        {
            if (UISellInventory == null) return;
            if (UISellInventory.gameObject.activeSelf)
            {
                HideSellInventory();
            }
            else
            {
                OpenSellInventory(shop);
            }
        }
    }
}
