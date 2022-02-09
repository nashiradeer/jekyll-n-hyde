using UnityEngine;

namespace JekyllHyde.UI.Manager
{
    public class MenuManager : MonoBehaviour
    {
        [field: SerializeField] private LoadingManager Loading { get; set; }

        private bool IsLoading { get; set; }

        public void StartGame()
        {
            if (!IsLoading)
            {
                IsLoading = true;
                Loading.StartLoad();
            }
        }

        public void ExitGame()
        {
            if (!IsLoading) Application.Quit();
        }
    }
}
