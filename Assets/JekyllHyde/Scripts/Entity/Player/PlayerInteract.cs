using JekyllHyde.World;
using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    // BLINDADO
    public class PlayerInteract : MonoBehaviour
    {
        private IInteractable InteractiveObject = null;
        private bool Interacting = false;

        private void FixedUpdate()
        {
            if ((Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return)) && !Interacting && InteractiveObject != null)
            {
                Debug.Log("PlayerInteract: Triggering interact...");
                Interacting = true;
                InteractiveObject.Interact();
            }
            else if (!(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return)) && Interacting)
            {
                Interacting = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (InteractiveObject == null && interactable != null) InteractiveObject = interactable;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (InteractiveObject == collision.GetComponent<IInteractable>()) InteractiveObject = null;
        }
    }
}
