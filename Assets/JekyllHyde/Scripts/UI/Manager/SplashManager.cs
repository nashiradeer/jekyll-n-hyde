using NashiraDeer.Splash;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JekyllHyde.UI.Manager
{
    public class SplashManager : MonoBehaviour
    {
        [field: SerializeField] private NashiraDeerSplash NashiraDeerSplash { get; set; }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(RunSplashes());
        }

        private IEnumerator RunSplashes()
        {
            yield return new WaitForSeconds(2f);
            yield return NashiraDeerSplash.StartSplash();
            SceneManager.LoadScene(1);
        }
    }
}
