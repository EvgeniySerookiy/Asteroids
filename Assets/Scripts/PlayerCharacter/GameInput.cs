using System;
using UnityEngine;
using Zenject;

namespace PlayerCharacter
{
    public class GameInput : ITickable
    {
        public event Action OnAccelerate;
        public event Action OnBreakAcceleration;
        public event Action OnTurnLeft;
        public event Action OnTurnRight;
        public event Action OnShootBullets;
        public event Action OnShootLaser;

        public void Tick()
        {
            if(Input.GetKey(KeyCode.W))
                OnAccelerate?.Invoke();
            else
                OnBreakAcceleration?.Invoke();
            
            if(Input.GetKey(KeyCode.A))
                OnTurnLeft?.Invoke();
            
            if(Input.GetKey(KeyCode.D))
                OnTurnRight?.Invoke();
            
            if(Input.GetKeyDown(KeyCode.Space))
                OnShootBullets?.Invoke();
            
            if(Input.GetKeyDown(KeyCode.M))
                OnShootLaser?.Invoke();
                
        }
    }
}