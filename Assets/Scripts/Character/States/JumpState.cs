using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class JumpState : IState
    {
        readonly int _jumpParameter = Animator.StringToHash("IsJumping");

        CharacterPhysic _controller;
        Animator _animator;

        float _initialJumpForce;
        public JumpState(CharacterPhysic controller, Animator animator, float jumpForce)
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
