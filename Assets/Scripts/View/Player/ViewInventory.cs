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
        public TextMeshProUGUI playerNameUIText;
        public TextMeshProUGUI goldUIText;

        private void Start()
        {
            inventory = GetComponent<Inventory>();
            inventory.ToggleUIInventory += ToggleInventory;
            inventory.HideUIInventory += HideInventory;
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

        void ToggleInventory()
        {
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

        public void OpenInventory()
        {
            ClearInventory();
            FillInventoryWithItems();
            ShowInventory();
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
    }
}
