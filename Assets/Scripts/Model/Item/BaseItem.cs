using UnityEngine;
using System.Collections.Generic;
using System;

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
