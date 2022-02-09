using UnityEngine;
using UnityEngine.SceneManagement;

namespace JekyllHyde.UI.Manager
{
    public class SplashManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(1);
        }
    }
}
