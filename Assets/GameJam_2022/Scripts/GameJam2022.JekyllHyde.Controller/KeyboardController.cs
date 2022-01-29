using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class KeyboardController : MonoBehaviour
    {
        [field: SerializeField] private PlayerController PlayerController { get; set; }
        
        private bool Enabled { get; set; }
        private float Speed = 2.0f;
        
        private void Start()
        {
            
        }

        private void Update()
        {
            if(Enabled)
                return;

            var move = new Vector3(Input.GetAxis("Horizontal"), 0);
            PlayerController.transform.position += move * (Speed * Time.deltaTime);
        }
    }
}