namespace JekyllHyde.Entity.Player
{
    public class PlayerSprite : EntitySprite
    {
        public void HideAnimation(bool hide)
        {
            AnimatorCtrl.SetBool("Hide", hide);
        }
    }
}
