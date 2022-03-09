using JekyllHyde.Entity.Player.Manager;

namespace JekyllHyde.UI
{
    public interface IUIMenu
    {
        PlayerManager CurrentPlayer { get; }
        bool IsOpen { get; }

        void Open(PlayerManager player);
        void Close();
    }
}