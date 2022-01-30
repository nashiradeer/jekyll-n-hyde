using DG.Tweening;
using GameJam2022.JekyllHyde.Manager;
using GameJam2022.JekyllHyde.Manager.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam2022.JekyllHyde.Scene
{
    public class MenuScene : MonoBehaviour
    {
        [field: SerializeField] private Button StartButton { get; set; }
        [field: SerializeField] private Button LeaveButton { get; set; }
        [field: SerializeField] private Image FadeImage { get; set; }
        
        private ISceneManager SceneManager { get; set; }
        
        private void Awake()
        {
            SceneManager = GameObject.FindWithTag("SceneManager").GetComponent<SceneManager>();
        }
        
        private void Start()
        {
            StartButton.onClick.AddListener(StartGame);
            LeaveButton.onClick.AddListener(LeaveGame);
            
            FadeImage.gameObject.SetActive(false);
        }

        private void StartGame()
        {
            FadeImage.gameObject.SetActive(true);
            FadeImage.DOFade(1, 1).OnComplete(() => { SceneManager.LoadScene("GamePlay");});
        }

        private void LeaveGame()
        {
            Application.Quit();
        }
    }
}