using UnityEngine;
using UnityEngine.SceneManagement;

namespace JekyllHyde.UI.Manager
{
    public class SplashManager : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}
