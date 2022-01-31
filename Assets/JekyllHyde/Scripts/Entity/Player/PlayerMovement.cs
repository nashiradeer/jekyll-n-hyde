using JekyllHyde.Entity;
using UnityEngine;

namespace JekyllHyde.Entity.Player
{
    public class PlayerMovement : EntityMovement
    {
        protected void FixedUpdate()
        {
            Move(Input.GetAxisRaw("Horizontal") * Speed);
        }
    }
}
