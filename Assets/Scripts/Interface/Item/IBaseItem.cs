using System.Collections.Generic;

namespace BGS
{
    public interface IBaseItem
    {
        public string ItemName { get; set; }
        public int Price { get; set; }
        public List<AnimationPathDictionary> AnimationPaths { get; set; }
        public string IconPath { get; set; }
        public ItemType Type { get; set; }
    }

    public interface IAnimationPathDictionary
    {
        public string Index { get; set; }
        public string AnimationPath { get; set; }
    }
}
