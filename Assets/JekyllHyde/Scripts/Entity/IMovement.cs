namespace JekyllHyde.Entity
{
    public interface IMovement
    {
        float Speed { get; set; }
        bool EnabledMovement { get; set; }
        void Move(float x);
    }
}
