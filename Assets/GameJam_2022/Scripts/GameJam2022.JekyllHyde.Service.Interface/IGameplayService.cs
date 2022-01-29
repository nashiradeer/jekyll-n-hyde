using GameJam2022.JekyllHyde.Domain.Interface;

namespace GameJam2022.JekyllHyde.Service.Interface
{
    public interface IGameplayService : IService
    {
        IPlayer Player { get; }
    }
}