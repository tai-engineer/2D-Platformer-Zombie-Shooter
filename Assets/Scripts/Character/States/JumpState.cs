using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class JumpState : IState
    {
        readonly int _jumpParameter = Animator.StringToHash("IsJumping");

        CharacterController _controller;
        Animator _animator;

        float _initialJumpForce;
        public JumpState(CharacterController controller, Animator animator, float jumpForce)
        {
            _controller = controller;
            _animator = animator;
            _initialJumpForce = jumpForce;
        }
        public void OnEnter()
        {
            _animator.SetBool(_jumpParameter, true);
            // Initial jump force
            _controller.SetVerticalMovement(_initialJumpForce);
            _controller.LandingPrepare();
        }

        public void OnExit()
        {
            _animator.SetBool(_jumpParameter, false);
        }

        public void Tick()
        {
            _controller.VerticalMove();
        }
    }
}
