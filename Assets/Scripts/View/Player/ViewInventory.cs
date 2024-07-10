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
        public Image headIcon;
        public Button unequipHeadEquipment;
        public Image torsoIcon;
        public Button unequipTorsoEquipment;
        public Image pantsIcon;
        public Button unequipPantsEquipment;
        public Image shoesIcon;
        public Button unequipShoesEquipment;

        [Header("Preview")]
        public Image previewHead;
        public Image previewTorso;
        public Image previewPants;
        public Image previewShoes;
        public string previewHeadPath;
        public string previewTorsoPath;
        public string previewPantsPath;
        public string previewShoesPath;

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
            UpdateSingleEquipmetInUI(inventory.Head, headIcon, unequipHeadEquipment);
            UpdateSingleEquipmetInUI(inventory.Torso, torsoIcon, unequipTorsoEquipment);
            UpdateSingleEquipmetInUI(inventory.Pants, pantsIcon, unequipPantsEquipment);
            UpdateSingleEquipmetInUI(inventory.Shoes, shoesIcon, unequipShoesEquipment);

            UpdateSingleCharacterPreview(inventory.Head, previewHead, previewHeadPath);
            UpdateSingleCharacterPreview(inventory.Torso, previewTorso, previewTorsoPath);
            UpdateSingleCharacterPreview(inventory.Pants, previewPants, previewPantsPath);
            UpdateSingleCharacterPreview(inventory.Shoes, previewShoes, previewShoesPath);

            ClearInventory();
            FillInventoryWithItems();
        }

        void UpdateSingleEquipmetInUI(BaseItem BodyPart, Image icon, Button unequipButton)
        {
            unequipButton.onClick.RemoveAllListeners();
            if (BodyPart != null)
            {
                icon.sprite = Resources.Load<Sprite>(BodyPart.IconPath);
                icon.color = Color.white;
                unequipButton.gameObject.SetActive(true);
                unequipButton.onClick.AddListener(() => {
                    inventory.UnequipItem(BodyPart);
                    UpdateEquipmentUI();
                });
            }
            else
            {
                icon.sprite = null;
                icon.color = new Color(255, 255, 255, 0);
                unequipButton.gameObject.SetActive(false);
            }
        }

        void UpdateSingleCharacterPreview(BaseItem BodyPart, Image icon, string path)
        {
            if (BodyPart != null)
                icon.sprite = Resources.Load<Sprite>(BodyPart.PreviewSpritePath);
            else
                icon.sprite = Resources.Load<Sprite>(path);
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
