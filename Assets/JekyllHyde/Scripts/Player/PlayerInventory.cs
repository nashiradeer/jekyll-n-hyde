using UnityEngine;

namespace JekyllHyde.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public const int DepositDoor = 0;
        public const int CockloftRoomDoor = 1;
        public const int BluePotion = 2;
        public const int RedPotion = 3;
        public const int Key1Picked = 4;
        public const int Key2Picked = 5;
        public const int Key3Picked = 6;
        public const int Key1Used = 7;
        public const int Key2Used = 8;

        public bool[] Inventory { get; private set; } = new bool[9];
    }
}
