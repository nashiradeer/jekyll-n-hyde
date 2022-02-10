using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JekyllHyde.UI.Manager
{
    public class LoadingManager : MonoBehaviour
    {
        [field: SerializeField] private Image LoadingScreen { get; set; }
        [field: SerializeField] private AudioSource Audio { get; set; }

        private AsyncOperation Loading { get; set; }

        public void StartLoad()
        {
            LoadingScreen.gameObject.SetActive(true);

            Loading = SceneManager.LoadSceneAsync(2);
            Loading.allowSceneActivation = false;
            StartCoroutine(LoadAnimation());
        }

        private IEnumerator LoadAnimation()
        {
            Audio.DOFade(0, 2);
            yield return LoadingScreen.DOFade(1, 2).WaitForCompletion();
            Loading.allowSceneActivation = true;
        }
    }
}
