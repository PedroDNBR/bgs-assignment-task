using UnityEngine;

namespace BGS
{
    public class ViewMenu : MonoBehaviour
    {
        public Transform menuUI;

        Menu menu;

        private void Start()
        {
            menu = GetComponent<Menu>();
            menu.ToggleMenu += ToggleMenu;
        }

        public void ToggleMenu() => menuUI.gameObject.SetActive(!menuUI.gameObject.activeSelf);
    }
}
