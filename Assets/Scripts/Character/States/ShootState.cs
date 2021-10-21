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

        float _coolDown = 0.5f;
        float _startTime;
        public ShootState(PlayerController controller, Animator animator)
        {
            _controller = controller;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_shootParameter, true);
            _startTime = 0f;
        }

        public void OnExit()
        {
            _animator.SetBool(_shootParameter, false);
        }

        public void Tick()
        {
            if(Mathf.Approximately(_startTime, 0f) || (Time.time - _startTime) >= _coolDown)
            {
                _controller.Shoot();
                _startTime = Time.time;
            }
        }
    }
}
