using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform CurrentPlayer = null;

        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (CurrentPlayer == null)
            {
                CurrentPlayer = GameObject.FindGameObjectWithTag("Player").transform;
            }
            else
            {
                transform.position = new Vector3(CurrentPlayer.position.x, CurrentPlayer.position.y, transform.position.z);
            }
        }
    }
}
