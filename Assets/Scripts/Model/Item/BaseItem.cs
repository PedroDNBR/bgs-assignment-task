using UnityEngine;

namespace BGS
{
    [CreateAssetMenu(menuName = "Game/BaseItem")]
    public class BaseItem : ScriptableObject, IBaseItem
    {
        [Header("Item Properties")]
        public string itemName;
        public int price;
        public string spritePath;
        public string animationPath;
        public string iconPath;
        public ItemType type;

        public string ItemName { get => itemName; set => itemName = value; }
        public int Price { get => price; set => price = value; }
        public string SpritePath { get => spritePath; set => spritePath = value; }
        public string AnimationPath { get => animationPath; set => animationPath = value; }
        public string IconPath { get => iconPath; set => iconPath = value; }
        public ItemType Type { get => type; set => type = value; }
    }
}
