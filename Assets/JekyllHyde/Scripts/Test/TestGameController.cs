using JekyllHyde.UI;
using UnityEngine;

namespace JekyllHyde.Test
{
    public class TestGameController : MonoBehaviour
    {
        [field: SerializeField] private KeypadController Keypad1;
        [field: SerializeField] private KeypadController Keypad2;

        private bool FullscreenLock = false;
        private bool QuitLock = false;

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape) && !QuitLock)
            {
                QuitLock = true;
                if (!Keypad1.KeypadEnabled && !Keypad2.KeypadEnabled) Application.Quit();
            }

            if (!Input.GetKey(KeyCode.Escape) && QuitLock)
            {
                QuitLock = false;
            }

            if (Input.GetKey(KeyCode.F11) && !FullscreenLock)
            {
                FullscreenLock = true;

                if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
                {
                    Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                }
                else
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }

            }

            if (!Input.GetKey(KeyCode.F11) && FullscreenLock)
            {
                FullscreenLock = false;
            }
        }
    }
}
