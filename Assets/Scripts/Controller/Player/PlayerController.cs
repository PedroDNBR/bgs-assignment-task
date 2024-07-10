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
        AnimatorController animatorController;

        void Start()
        {
            movement = GetComponent<Movement>();
            inventory = GetComponent<Inventory>();
            viewInventory = GetComponent<ViewInventory>();
            animatorController = GetComponentInChildren<AnimatorController>();
        }

        private void Update()
        {
            Debug.Log(Input.GetAxis("Horizontal"));

            if (Input.GetAxis("Horizontal") > 0.8f)
                horizontal = 1;
            else if (Input.GetAxis("Horizontal") < -0.9f)
                horizontal = -1;
            else horizontal = 0;

            if (Input.GetAxis("Vertical") > 0.8f)
                vertical = 1;
            else if (Input.GetAxis("Vertical") < -0.9f)
                vertical = -1;
            else vertical = 0;

            animatorController.UpdateAnimationAxis(horizontal, vertical);

            if (Input.GetButtonDown("Interact"))
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

