using UnityEngine;

namespace JekyllHyde.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public const int DepositDoor = 0;
        public const int CockloftRoomDoor = 1;

        public bool[] Inventory { get; private set; } = new bool[2];
    }
}
