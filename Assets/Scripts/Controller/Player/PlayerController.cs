using UnityEngine;

namespace BGS
{
    public class PlayerController : MonoBehaviour
    {
        float vertical;
        float horizontal;

        Movement movement;
        Inventory inventory;
        ViewInventory viewInventory;
        ViewShop shopNearby;

        void Start()
        {
            movement = GetComponent<Movement>();
            inventory = GetComponent<Inventory>();
            viewInventory = GetComponent<ViewInventory>();
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
                inventory.HideInventoy();
                shopNearby.ToggleShop(inventory);
                inventory.ToggleSellInventory(shopNearby.Shop);
            }
            if (shopNearby == null)
            {
                inventory.ToggleInventory();
            }
        }

        void FixedUpdate()
        {
            movement.MoveCharacter(horizontal, vertical, Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ViewShop shop = collision.GetComponent<ViewShop>();
            if (shopNearby)
            {
                ClearAndCloseNearbyShop();
            }
            if (shop != null)
            {
                shopNearby = shop;
                shopNearby.Shop.purchasedItemWentToInventory += viewInventory.OpenSellInventory;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ViewShop shop = collision.GetComponent<ViewShop>();
            if (shopNearby != null && shopNearby.Shop != null && shop != null && shopNearby == shop && inventory != null)
            {
                ClearAndCloseNearbyShop();
                inventory.HideSellInventory();
            }
        }

        void ClearAndCloseNearbyShop()
        {
            shopNearby.HideShop();
            shopNearby.Shop.purchasedItemWentToInventory -= viewInventory.OpenSellInventory;
            shopNearby = null;
        }
    }
}

