namespace JekyllHyde.Entity.Player.Mechanics
{
    public static class PlayerInventory
    {
        /** All indexes used by the inventory for the game items
         * 
         * Deposit Door Unlocked       = 0
         * Cockloft Room Door Unlocked = 1
         * Blue Potion Picked          = 2
         * Red Potion Picked           = 3
         * Key 1 Picked                = 4
         * Key 2 Picked                = 5
         * Key 3 Picked                = 6
         * Key 1 Used                  = 7
         * Key 2 Used                  = 8
         */

        public static bool[] Items { get; private set; } = new bool[9];
    }
}
