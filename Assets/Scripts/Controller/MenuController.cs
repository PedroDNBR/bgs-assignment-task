using UnityEngine;

namespace BGS
{
    public class MenuController : MonoBehaviour
    {
        Menu menu;

        private void Start() => menu = GetComponent<Menu>();

        private void Update()
        {
            if(Input.GetButtonDown("Escape"))
                menu.ToggleMenuUI();
        }
    }
}
