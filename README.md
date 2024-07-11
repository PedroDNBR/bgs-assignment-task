# Blue Gravity Studios Assignment task

[PDF Document](https://github.com/PedroDNBR/bgs-assignment-task/blob/master/explanation-document.pdf)

[Game Download]("https://github.com/PedroDNBR/bgs-assignment-task/blob/master/PEDRO_GOMES_ANTUNES_BGS_TASK.rar")

### System explanation:
The game uses the MVC pattern. In the shop, a List with a custom class that stores the items and  its quantity, which is then converted to a Dictionary<Item, int>, where ’Item’ is the ScriptableObject, and ’int’ the quantity. ’Item’ has information about the item and paths for sprites and animations. The player has basic 2D top-down movement and an OnTriggerEnter hitbox for detecting when the Shopkeeper is in range. When in range, a tooltip appears above the Shopkeeper, prompting the player to press 'E' to interact, opening Buy and Sell menus. When buying, it checks if the player has enough gold and if the item is in stock. If all the requirements are fulfilled, the item is added to the player's inventory Dictionary<Item, int> and subtracted from the shop. Equipping an item moves it from the inventory to the equipment slot, there, a function calls the AnimatorController to override the current animations with those from the ScriptableObject. It searches the Animator animation clips, finds the one with the same index as in the ScriptableObject, and replaces it with the new animation, then sets this updated Animator in the player's Animator. It also has a preview in the inventory screen where the player character is visible, and for each item equipped or unequipped, it updates the sprites in the preview. For selling, the item is removed from the player's inventory, added back to the shop, and the player receives the item's coin value, if the item is equipped, it will not show in the player inventory since it is in the equipment slot, the player must unequip it in order to sell the item.

### Thought process during the interview
I believe this refers to the thought process during game development. So, here's mine: I began by drawing the base sprites for the game. Next, I coded the player's movement, designed the UI, created ScriptableObjects, and built the buy/sell system. After researching, I found this [youtuber](https://youtu.be/PNWK5o9l54w), in his video he showed about the AnimatorOverride, which I used as a foundation for my implementation. After integrating and adjusting it, I made more sprites and ScriptableObjects. Finally, I debugged and made additional UI adjustments.

### Personal assessment
The code is solid and expandable, but it has limitations due to development time.
