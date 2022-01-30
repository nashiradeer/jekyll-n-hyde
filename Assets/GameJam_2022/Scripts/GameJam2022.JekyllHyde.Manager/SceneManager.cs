using GameJam2022.JekyllHyde.Manager.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJam2022.JekyllHyde.Manager
{
    public class SceneManager : MonoBehaviour, ISceneManager
    {
        
        public void LoadScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += operation =>
            {
                Resources.UnloadUnusedAssets();
            };
        }

        public void LoadOverlay(string sceneName)
        {
            throw new System.NotImplementedException();
        }

        public void UnloadOverlay()
        {
            throw new System.NotImplementedException();
        }
    }
}