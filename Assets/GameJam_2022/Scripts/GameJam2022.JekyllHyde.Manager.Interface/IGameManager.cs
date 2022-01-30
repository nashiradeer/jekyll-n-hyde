using GameJam2022.JekyllHyde.Service.Interface;

namespace GameJam2022.JekyllHyde.Manager.Interface
{
    public interface IGameManager
    {
        void Init();
        IGameplayService GameplayService { get; }
    }
}