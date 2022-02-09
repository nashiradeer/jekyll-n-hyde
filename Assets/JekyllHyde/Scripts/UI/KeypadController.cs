using JekyllHyde.Entity.Player.Manager;
using JekyllHyde.UI.Manager;
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
        [field: SerializeField] private PlayerManager PlayerManager { get; set; }

        private bool DisableWrite { get; set; }
        private string CorrectKey { get; set; }
        private string CurrentKey { get; set; }
        public bool KeypadEnabled { get; set; }

        public UnityEvent OnKeyCorrected = new UnityEvent();

        public void Open(string correctKey)
        {
            if (KeypadEnabled) return;
            PlayerManager.Mechanics(false);

            CorrectKey = correctKey;

            DisableWrite = false;
            KeypadEnabled = true;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (!KeypadEnabled) return;
            OnKeyCorrected.RemoveAllListeners();
            KeypadEnabled = false;

            CorrectKey = "";
            CurrentKey = "";

            PlayerManager.Mechanics(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameObject.SetActive(false);
        }

        public void Write(string code)
        {
            if (!KeypadEnabled && !DisableWrite) return;

            string newKey = CurrentKey += code;
            Debug.Log($"KeypadController: Write requested, result {newKey}.");
            
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
            DisableWrite = true;

            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);

            OnKeyCorrected.Invoke();
            KeypadUI.sprite = Default;

            Close();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && KeypadEnabled) Close();
        }
    }
}
