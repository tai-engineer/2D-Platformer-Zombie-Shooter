using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class MoveState : IState
    {
        readonly int _sprintParameter = Animator.StringToHash("IsSprinting");
        readonly int _moveParameter = Animator.StringToHash("IsMoving");

        CharacterController _controller;
        PlayerCharacter _player;
        Animator _animator;
        public MoveState(CharacterController controller, PlayerCharacter player, Animator animator)
        {
            _controller = controller;
            _animator = animator;
            _player = player;
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
            _controller.HorizontalMove(_player.MoveInput.x);
        }
    }
}
