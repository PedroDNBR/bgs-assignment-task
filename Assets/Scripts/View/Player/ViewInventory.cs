using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGS
{
    public class ViewInventory : MonoBehaviour
    {
        Inventory inventory;

        [Header("UI elements")]
        public GameObject UIItemTemplate;
        public VerticalLayoutGroup UIItemGrid;
        public TextMeshProUGUI playerNameUIText;
        public TextMeshProUGUI goldUIText;

        private void Start()
        {
            inventory = GetComponent<Inventory>();
        }
    }
}
