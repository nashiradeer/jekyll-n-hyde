using System;
using UnityEngine;

namespace GameJam2022.JekyllHyde.Controller
{
    public class KeyboardController : MonoBehaviour
    {
        private bool Enabled { get; set; }
        
        public event Action OnMove
        {
            add => _onMove += value;
            remove => _onMove -= value;
        }

        private Action _onMove { get; set; }
        
        private void Start()
        {
            
        }

        private void Update()
        {
            if(Enabled)
                return;

            if(Input.GetAxis("Horizontal") != 0f)
                _onMove?.Invoke();
        }
    }
}