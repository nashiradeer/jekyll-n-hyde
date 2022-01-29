using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] private SpriteRenderer PlayerSprite { get; set; }
        private float Speed = 2.0f;
        
        public void Move()
        {
            var axis = Input.GetAxis("Horizontal");
            var move = new Vector3(axis, 0);
            transform.position += move * (Speed * Time.deltaTime);
            PlayerSprite.transform.Rotate(new Vector3(0, axis));
        }
    }
}