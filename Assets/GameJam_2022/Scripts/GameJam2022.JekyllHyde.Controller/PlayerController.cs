using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Collider2D InteractiveObject = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (InteractiveObject == null && collision.tag == "Interactable") InteractiveObject = collision;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (InteractiveObject == collision) InteractiveObject = null;
        }
    }
}