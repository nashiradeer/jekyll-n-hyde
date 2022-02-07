using UnityEngine;

namespace JekyllHyde.Entity.Player.World
{
    public class PlayerCamera : MonoBehaviour
    {
        [field: SerializeField] private Transform Player = null;
        [field: SerializeField] public float LeftX { get; set; }
        [field: SerializeField] public float RightX { get; set; }
        [field: SerializeField] public float CameraY { get; set; }

        private void FixedUpdate()
        {
            if (LeftX != RightX && Player.position.x > RightX && Player.position.x < LeftX)
            {
                transform.position = new Vector3(Player.position.x, CameraY, transform.position.z);
            }
            else if (LeftX != RightX && Player.position.x < RightX)
            {
                transform.position = new Vector3(RightX, CameraY, transform.position.z);
            }
            else if (LeftX != RightX && Player.position.x > LeftX)
            {
                transform.position = new Vector3(LeftX, CameraY, transform.position.z);
            }
        }
    }
}
