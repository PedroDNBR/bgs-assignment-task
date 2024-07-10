using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS
{
    public class ViewInventory : MonoBehaviour
    {
        Inventory inventory;

        [Header("UI")]
        public Transform UIInventory;
        public GameObject UIItemTemplate;
        public VerticalLayoutGroup UIItemGrid;
        public TextMeshProUGUI UIGoldText;

        [Header("UI Sell")]
        public Transform UISellInventory;
        public GameObject UISellItemTemplate;
        public VerticalLayoutGroup UISellItemGrid;
        public TextMeshProUGUI UIGoldSellText;

        [Header("UI Equipment")]
        public Image HeadIcon;
        public Button UnequipHeadEquipment;
        public Image TorsoIcon;
        public Button UnequipTorsoEquipment;
        public Image PantsIcon;
        public Button UnequipPantsEquipment;
        public Image ShoesIcon;
        public Button UnequipShoesEquipment;


        private void Start()
        {
            inventory = GetComponent<Inventory>();
            inventory.ToggleUIInventory += ToggleInventory;
            inventory.HideUIInventory += HideInventory;
            inventory.ToggleUISellInventory += ToggleSellInventory;
            inventory.HideUISellInventory += HideSellInventory;
            inventory.EquippedUIItem += UpdateEquipmentUI;

            UpdateEquipmentUI();
        }

        public void FillInventoryWithItems()
        {
            foreach (KeyValuePair<IBaseItem, int> item in inventory.ItemsInInventory)
            {
                GameObject newUIItemTemplate = Instantiate(UIItemTemplate, UIItemGrid.transform);

                // Creates the UI template using the items value
                item.Key.SetItemForEquipInUI(
                    newUIItemTemplate,
                    item.Value,
                    "Equip",
                    () => inventory.Equip(item.Key)
                );

                // Activate template
                newUIItemTemplate.SetActive(true);
            }
        }

        public void FillSellInventoryWithItems(Shop shop)
        {
            foreach (KeyValuePair<IBaseItem, int> item in inventory.ItemsInInventory)
            {
                GameObject newUIItemTemplate = Instantiate(UISellItemTemplate, UISellItemGrid.transform);

                // Creates the UI template using the items value
                item.Key.SetItemForShopInUI(
                    newUIItemTemplate,
                    item.Value,
                    "Sell",
                    () => inventory.SellItem(item.Key, shop)
                );

                // Activate template
                newUIItemTemplate.SetActive(true);
            }
        }

        void UpdateEquipmentUI()
        {
            UpdateSingleEquipmetInUI(inventory.Head, HeadIcon, UnequipHeadEquipment);
            UpdateSingleEquipmetInUI(inventory.Torso, TorsoIcon, UnequipTorsoEquipment);
            UpdateSingleEquipmetInUI(inventory.Pants, PantsIcon, UnequipPantsEquipment);
            UpdateSingleEquipmetInUI(inventory.Shoes, ShoesIcon, UnequipShoesEquipment);
            ClearInventory();
            FillInventoryWithItems();
        }

        void UpdateSingleEquipmetInUI(BaseItem BodyPart, Image Icon, Button UnequipButton)
        {
            UnequipButton.onClick.RemoveAllListeners();
            if (BodyPart != null)
            {
                Icon.sprite = Resources.Load<Sprite>(BodyPart.IconPath);
                Icon.color = Color.white;
                UnequipButton.gameObject.SetActive(true);
                UnequipButton.onClick.AddListener(() => {
                    inventory.UnequipItem(BodyPart);
                    UpdateEquipmentUI();
                });
            }
            else
            {
                Icon.sprite = null;
                Icon.color = new Color(255, 255, 255, 0);
                UnequipButton.gameObject.SetActive(false);
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
