using JekyllHyde.Entity.Player.Manager;
using JekyllHyde.UI;
using JekyllHyde.World.Interaction;
using JekyllHyde.World.Manager;
using UnityEngine;

namespace JekyllHyde.Entity.Player.Mechanics
{
    // BLINDADO
    public class PlayerInteract : MonoBehaviour
    {
        [field: SerializeField] public PlayerManager Manager { get; private set; }
        [field: SerializeField] public bool EnabledInteract { get; set; }
        [field: SerializeField] public KeypadController Keypad1 { get; set; }
        [field: SerializeField] public KeypadController Keypad2 { get; set; }

        [field: SerializeField] private QuestManager QuestManager { get; set; }
        [field: SerializeField] private DialogManager DialogManager { get; set; }

        private IInteractable InteractiveObject = null;
        private bool Interacting = false;

        private void FixedUpdate()
        {
            if (IsInteracting(out bool alternativeKey) && EnabledInteract && !Interacting && InteractiveObject != null)
            {
                Debug.Log($"PlayerInteract: Triggering interact... (Alternative key? {alternativeKey})");

                Interacting = true;

                if (InteractiveObject.MinimumStep <= QuestManager.Step) InteractiveObject.Interact(this, alternativeKey);
                else DialogManager.Show("Nao tenho nada para ver aqui agora", 0.5f, 0.4f);
            }
            else if (!IsInteracting(out _) && Interacting)
            {
                Interacting = false;
            }
        }

        private bool IsInteracting(out bool alternativeKey)
        {
            bool normalKey = Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Return);
            bool altKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

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
