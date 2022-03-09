using JekyllHyde.Entity.Player.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JekyllHyde.UI
{
    public class KeypadController : MonoBehaviour, IUIMenu
    {
        [field: SerializeField] private Image KeypadUI { get; set; }
        [field: SerializeField] private Sprite Default { get; set; }
        [field: SerializeField] private Sprite Correct { get; set; }
        [field: SerializeField] private AudioManager AudioManager { get; set; }

        private bool DisableWrite { get; set; }
        private string CorrectKey { get; set; }
        private string CurrentKey { get; set; }

        public PlayerManager CurrentPlayer { get; private set; }
        public bool IsOpen { get; private set; }

        public UnityEvent OnKeyCorrected = new UnityEvent();

        public void Init(string correctKey)
        {
            CorrectKey = correctKey;
        }

        public void Open(PlayerManager player)
        {
            if (IsOpen) return;

            player.Mechanics(false);
            CurrentPlayer = player;

            DisableWrite = false;
            IsOpen = true;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (!IsOpen) return;

            OnKeyCorrected.RemoveAllListeners();
            IsOpen = false;

            CorrectKey = "";
            CurrentKey = "";

            CurrentPlayer.Mechanics(true);
            CurrentPlayer = null;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameObject.SetActive(false);
        }

        public void Write(string code)
        {
            if (!IsOpen && !DisableWrite) return;

            string newKey = CurrentKey += code;
            AudioManager.KeypadClick.Play();
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
            
            CurrentPlayer.CloseMenu();
        }
    }
}
