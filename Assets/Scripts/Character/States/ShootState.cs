using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class ShootState : IState
    {
        readonly int _shootParameter = Animator.StringToHash("IsShooting");

        PlayerController _controller;
        Animator _animator;
        public ShootState(PlayerController controller, Animator animator)
        {
            _controller = controller;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_shootParameter, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_shootParameter, false);
        }

        public void Tick()
        {
            
        }
    }
}
