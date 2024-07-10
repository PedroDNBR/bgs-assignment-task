using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using static UnityEditor.Progress;
using System.Xml.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace BGS
{
    [CreateAssetMenu(menuName = "Game/BaseItem")]
    public class BaseItem : ScriptableObject, IBaseItem
    {
        [Header("Item Properties")]
        public string itemName;
        public int price;
        public List<AnimationPathDictionary> animationPaths = new List<AnimationPathDictionary>();
        public string iconPath;
        public ItemType type;

        public string ItemName { get => itemName; set => itemName = value; }
        public int Price { get => price; set => price = value; }
        public List<AnimationPathDictionary> AnimationPaths { get => animationPaths; set => animationPaths = value; }
        public string IconPath { get => iconPath; set => iconPath = value; }
        public ItemType Type { get => type; set => type = value; }

        public delegate void ItemAction();

        public void SetItemForShopInUI(
            GameObject UIElement,
            int quantity,
            string buttonText,
            Action action
        )
        {
            // Prints the item name
            SetItemNameInUI(UIElement);

            // Prints the item Quantity
            SetItemQuantityInUI(UIElement, quantity);

            // Prints the item price
            SetItemPriceInUI(UIElement);

            // Button event to purchase the item
            SetItemButtonAction(UIElement, buttonText, action);

            // SetIcon
            SetItemIconInUI(UIElement);
        }

        public void SetItemForEquipInUI(
            GameObject UIElement,
            int quantity,
            string buttonText,
            Action action
        )
        {
            // Prints the item name
            SetItemNameInUI(UIElement);

            // Prints the item Quantity
            SetItemQuantityInUI(UIElement, quantity);

            // Button event to purchase the item
            SetItemButtonAction(UIElement, buttonText, action);

            // SetIcon
            SetItemIconInUI(UIElement);
        }

        public void SetItemNameInUI(GameObject UIElement)
        {
            UIElement.GetComponentsInChildren<TextMeshProUGUI>()[0].text = $"{ItemName}";
        }

        public void SetItemQuantityInUI(GameObject UIElement, int quantity)
        {
            UIElement.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"x{quantity}";
        }

        public void SetItemPriceInUI(GameObject UIElement)
        {
            UIElement.GetComponentsInChildren<TextMeshProUGUI>()[2].text = $"{Price} Gold";
        }

        public void SetItemButtonAction(GameObject UIElement, string buttonText, Action action)
        {
            Button button = UIElement.GetComponentInChildren<Button>();

            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;

            button.onClick.AddListener(() => action?.Invoke());
        }

        public void SetItemIconInUI(GameObject UIElement)
        {
            UIElement.GetComponentsInChildren<Image>()[2].
                sprite = Resources.Load<Sprite>(IconPath);
        }
    }

    [Serializable]
    public class AnimationPathDictionary : IAnimationPathDictionary
    {
        public string index;
        public string animationPath;

        public string Index { get => index; set => index = value; }
        public string AnimationPath { get => animationPath; set => animationPath = value; }
    }
}
