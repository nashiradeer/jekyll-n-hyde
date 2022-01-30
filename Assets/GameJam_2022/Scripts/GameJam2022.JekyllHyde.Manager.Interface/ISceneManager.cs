namespace GameJam2022.JekyllHyde.Manager.Interface
{
    public interface ISceneManager : IManager
    {
        void LoadScene(string sceneName);
        void LoadOverlay(string sceneName);
        void UnloadOverlay();
    }
}