using JekyllHyde.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JekyllHyde.UI
{
    public class KeypadController : MonoBehaviour
    {
        [field: SerializeField] private Image KeypadUI { get; set; }
        [field: SerializeField] private Sprite Default { get; set; }
        [field: SerializeField] private Sprite Correct { get; set; }
        [field: SerializeField] private PlayerMovement Movement { get; set; }
        [field: SerializeField] private PlayerInteract Interaction { get; set; }
        [field: SerializeField] private PlayerHide Hide { get; set; }

        private string CorrectKey { get; set; }
        private string CurrentKey { get; set; }
        public bool KeypadEnabled { get; set; }

        public UnityEvent OnKeyCorrected = new UnityEvent();

        public void Open(string correctKey)
        {
            Movement.EnabledMovement = false;
            Interaction.EnabledInteract = false;
            Hide.EnabledHide = false;

            CorrectKey = correctKey;

            KeypadEnabled = true;

            gameObject.SetActive(true);
        }

        public void Close()
        {
            OnKeyCorrected.RemoveAllListeners();
            KeypadEnabled = false;

            CorrectKey = "";
            CurrentKey = "";

            gameObject.SetActive(false);

            Movement.EnabledMovement = true;
            Interaction.EnabledInteract = true;
            Hide.EnabledHide = true;
        }

        public void Write(string code)
        {
            if (!KeypadEnabled) return;

            string newKey = CurrentKey += code;
            
            if (newKey.Length >= CorrectKey.Length)
            {
                if (newKey == CorrectKey) StartCoroutine(CorrectKeyAnim());
                else CurrentKey = "";
            }
            else
            {
                CurrentKey = newKey;
            }
        }

        private IEnumerator CorrectKeyAnim()
        {
            KeypadUI.sprite = Correct;
            KeypadEnabled = false;

            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);

            OnKeyCorrected.Invoke();
            KeypadUI.sprite = Default;

            Close();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape) && KeypadEnabled) Close();
        }
    }
}
