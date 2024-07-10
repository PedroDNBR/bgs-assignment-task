using System;
using UnityEngine;

namespace BGS
{
    public class Menu : MonoBehaviour
    {
        public event Action ToggleMenu;

        public void QuitGame() => Application.Quit();

        public void ToggleMenuUI() => ToggleMenu?.Invoke();
    }
}
