namespace BGS
{
    public interface IBaseItem
    {
        public string ItemName { get; set; }
        public int Price { get; set; }
        public string SpritePath { get; set; }
        public string AnimationPath { get; set; }
        public string IconPath { get; set; }
        public ItemType Type { get; set; }
    }
}
