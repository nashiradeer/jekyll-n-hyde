using GameJam2022.JekyllHyde.Manager.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace GameJam2022.JekyllHyde.Manager
{
    public class SceneManager : MonoBehaviour, ISceneManager
    {
        
        public void LoadScene(string sceneName)
        {
            UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += operation =>
            {
                Resources.UnloadUnusedAssets();
            };
        }

        public void LoadOverlay(string sceneName)
        {
            UnitySceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += operation =>
            {
                Resources.UnloadUnusedAssets();
            };
        }

        public void UnloadOverlay()
        {
            UnitySceneManager.UnloadSceneAsync(UnitySceneManager.GetActiveScene());
        }
    }
}