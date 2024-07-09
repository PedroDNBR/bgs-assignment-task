using UnityEngine;

namespace BGS
{
    public class PlayerController : MonoBehaviour
    {
        float vertical;
        float horizontal;

        Movement movement;
        Inventory inventory;
        ViewShop shopNearby;

        void Start()
        {
            movement = GetComponent<Movement>();
            inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if(Input.GetButtonDown("Interact"))
            {
                Interact();
            }
        }

        void Interact()
        {
            if(shopNearby != null)
            {
                shopNearby.OpenShop(inventory);
            }
        }

        void FixedUpdate()
        {
            movement.MoveCharacter(horizontal, vertical, Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ViewShop shop = collision.GetComponent<ViewShop>();
            Debug.Log(shop);
            if (shopNearby)
            {
                ClearAndCloseNearbyShop();
            }
            if (shop != null)
            {
                shopNearby = shop;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ViewShop shop = collision.GetComponent<ViewShop>();
            Debug.Log(shop);
            if (shopNearby != null && shop != null && shopNearby == shop)
            {
                ClearAndCloseNearbyShop();
            }
        }

        void ClearAndCloseNearbyShop()
        {
            shopNearby.HideShop();
            shopNearby = null;
        }
    }
}

