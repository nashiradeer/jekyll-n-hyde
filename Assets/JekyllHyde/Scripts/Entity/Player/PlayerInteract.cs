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
            if (IsInteracting(out bool alternativeKey) && !Interacting && InteractiveObject != null)
            {
                Debug.Log("PlayerInteract: Triggering interact...");
                Interacting = true;
                InteractiveObject.Interact(alternativeKey);
            }
            else if (!IsInteracting(out _) && Interacting)
            {
                Interacting = false;
            }
        }

        private bool IsInteracting(out bool alternativeKey)
        {
            bool normalKey = Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return);
            bool altKey = Input.GetAxisRaw("Vertical") == 1;
            if (!normalKey) alternativeKey = altKey;
            else alternativeKey = false;
            return normalKey || altKey;
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
