using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public const int DEPOSIT_LOCK = 0;
        public const int COCKLOFTROOM_LOCK = 1;

        public bool[] Inventory { get; private set; } = new bool[2];
    }
}
