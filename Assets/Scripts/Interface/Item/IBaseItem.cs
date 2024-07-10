using System;
using System.Collections.Generic;
using UnityEngine;

namespace BGS
{
    public interface IBaseItem
    {
        public string ItemName { get; set; }
        public int Price { get; set; }
        public List<AnimationPathDictionary> AnimationPaths { get; set; }
        public string IconPath { get; set; }
        public ItemType Type { get; set; }

        public void SetItemNameInUI(GameObject UIElement);

        public void SetItemQuantityInUI(GameObject UIElement, int quantity);

        public void SetItemPriceInUI(GameObject UIElement);

        public void SetItemButtonAction(GameObject UIElement, string buttonText, Action action);

        public void SetItemIconInUI(GameObject UIElement);
    }

    public interface IAnimationPathDictionary
    {
        public string Index { get; set; }
        public string AnimationPath { get; set; }
    }
}
