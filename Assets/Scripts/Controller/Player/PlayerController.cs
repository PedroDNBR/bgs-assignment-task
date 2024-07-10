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

        public Transform menuUI;

        void Start()
        {
            movement = GetComponent<Movement>();
            inventory = GetComponent<Inventory>();
            viewInventory = GetComponent<ViewInventory>();
            animatorController = GetComponentInChildren<AnimatorController>();
            inventory.EquipmentEquipped += animatorController.OverrideAnimation;
        }

        private void Update()
        {
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

            if (Input.GetButtonDown("Interact")) Interact();

            if (Input.GetButtonDown("Escape")) OpenMenu();

        }

        void Interact()
        {
            if (menuUI.gameObject.activeSelf) return;
            if(shopNearby != null)
            {
                inventory.HideInventoy();
                shopNearby.ToggleShop(inventory);
                inventory.ToggleSellInventory(shopNearby.Shop);
            }
            else
            {
                inventory.ToggleInventory();
            }
        }

        void OpenMenu()
        {
            if (shopNearby != null)
            {
                inventory.HideInventoy();
                shopNearby.HideShop();
                inventory.HideSellInventory();
            }
            inventory.HideInventoy();
        }

        void FixedUpdate()
        {
            movement.MoveCharacter(horizontal, vertical, Time.fixedDeltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ViewShop shop = collision.GetComponent<ViewShop>();
            if (shop != null && shopNearby)
            {
                ClearAndCloseNearbyShop();
            }
            if (shop != null)
            {
                shopNearby = shop;
                shopNearby.ShowInteractionIcon();
                shopNearby.Shop.purchasedItemWentToInventory += viewInventory.OpenSellInventory;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ViewShop shop = collision.GetComponent<ViewShop>();
            if (shop != null) shop.HideInteractionIcon();

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

