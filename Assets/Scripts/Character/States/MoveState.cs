using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class MoveState : IState
    {
        readonly int _sprintParameter = Animator.StringToHash("IsSprinting");
        readonly int _moveParameter = Animator.StringToHash("IsMoving");

        PlayerController _controller;
        Animator _animator;
        public MoveState(PlayerController controller, Animator animator)
        {
            _controller = controller;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_moveParameter, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_moveParameter, false);
        }

        public void Tick()
        {
            _controller.HorizontalMove();
        }
    }
}
