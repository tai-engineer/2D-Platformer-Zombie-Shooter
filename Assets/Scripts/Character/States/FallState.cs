using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class FallState : IState
    {
        static int _fallParameter = Animator.StringToHash("IsFalling");

        Animator _animator;
        CharacterController _controller;
        public FallState(CharacterController controller, Animator animator)
        {
            _controller = controller;
            _animator = animator;
        }
        public void OnEnter()
        {
            _animator.SetBool(_fallParameter, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_fallParameter, false);
        }

        public void Tick()
        {
            _controller.VerticalMove();
            if (Mathf.Approximately(_controller.MoveVector.y, _controller.Gravity))
            {
                _controller.SetVerticalMovement(_controller.Gravity);
            }
        }
    }
}
