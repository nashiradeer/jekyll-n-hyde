using UnityEngine;

namespace JekyllHyde.Entity.Player.World
{
    public class PlayerCamera : MonoBehaviour
    {
        [field: SerializeField] private Transform Player = null;
        [field: SerializeField] public float LeftX { get; set; }
        [field: SerializeField] public float RightX { get; set; }
        [field: SerializeField] public float CameraY { get; set; }

        private void LateUpdate()
        {
            transform.position = new Vector3(Mathf.Clamp(Player.position.x, RightX, LeftX), CameraY, transform.position.z);
        }
    }
}
