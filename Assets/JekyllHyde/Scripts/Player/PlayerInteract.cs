using DG.Tweening;
using JekyllHyde.UI;
using JekyllHyde.World;
using System.Collections;
using UnityEngine;
using TMPro;

namespace JekyllHyde.Player
{
    // BLINDADO
    public class PlayerInteract : MonoBehaviour
    {
        [field: SerializeField] public bool EnabledInteract { get; set; }
        [field: SerializeField] public KeypadController Keypad1 { get; private set; }
        [field: SerializeField] public KeypadController Keypad2 { get; private set; }
        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public TMP_Text DialogText { get; private set; }
        [field: SerializeField] public QuestManager Quest { get; private set; }

        private IInteractable InteractiveObject = null;
        private bool Interacting = false;

        private void FixedUpdate()
        {
            if (IsInteracting(out bool alternativeKey) && EnabledInteract && !Interacting && InteractiveObject != null)
            {
                if (InteractiveObject.MinimumQuest <= Quest.Step)
                {
                    Debug.Log("PlayerInteract: Triggering interact...");
                    Interacting = true;
                    InteractiveObject.Interact(this, alternativeKey);
                }
                else StartCoroutine(GameplayStart());
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

        private IEnumerator GameplayStart()
        {
            EnabledInteract = false;
            Movement.EnabledMovement = false;

            DialogText.text = "Não tenho nada para ver aqui agora";

            yield return DialogText.DOFade(1, 0.4f).WaitForCompletion();
            yield return new WaitForSeconds(0.7f);
            yield return DialogText.DOFade(0, 0.4f).WaitForCompletion();

            EnabledInteract = true;
            Movement.EnabledMovement = true;
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
