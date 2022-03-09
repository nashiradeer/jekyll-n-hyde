using DG.Tweening;
using NashiraDeer.Splash;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JekyllHyde.UI.Manager
{
    public class SplashManager : MonoBehaviour
    {
        [field: SerializeField] private NashiraDeerSplash NashiraDeerSplash { get; set; }
        [field: SerializeField] private Image HidoiImage { get; set; }

        private AsyncOperation LoadingScene { get; set; }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            LoadingScene = SceneManager.LoadSceneAsync(1);
            LoadingScene.allowSceneActivation = false;

            StartCoroutine(RunSplashes());
        }

        private IEnumerator RunSplashes()
        {
            HidoiImage.gameObject.SetActive(true);
            yield return HidoiImage.DOFade(1, 2f).WaitForCompletion();
            yield return new WaitForSeconds(1.5f);
            yield return HidoiImage.DOFade(0, 2f).WaitForCompletion();
            HidoiImage.gameObject.SetActive(false);

            NashiraDeerSplash.gameObject.SetActive(true);
            yield return NashiraDeerSplash.StartSplash();
            NashiraDeerSplash.gameObject.SetActive(false);

            LoadingScene.allowSceneActivation = true;
        }
    }
}
