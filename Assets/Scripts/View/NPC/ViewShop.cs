using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGS
{
    public class ViewShop : MonoBehaviour
    {
        Shop shop;

        private void Start()
        {
            shop = GetComponent<Shop>();
        }
    }
}