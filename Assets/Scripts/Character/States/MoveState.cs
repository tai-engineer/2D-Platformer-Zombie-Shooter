using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class MoveState : IState
    {
        static int _sprintParameter = Animator.StringToHash("IsSprinting");
        static int _moveParameter = Animator.StringToHash("IsMoving");

        PlayerController _controller;
        Animator _animator;
        public MoveState(PlayerController controller, Animator animator)
        {
            _controller = controller;
            _animator = animator;
        }
        public void OnEnter()
        {
            Debug.Log($"Enter {this.GetType()}");
            _animator.SetBool(_moveParameter, true);
        }

        public void OnExit()
        {
            Debug.Log($"Exit {this.GetType()}");
            _animator.SetBool(_moveParameter, false);
        }

        public void Tick()
        {
            Debug.Log($"Tick {this.GetType()}");
            _controller.MoveHorizontal();
            _controller.VerticalCollisionCheck(false);
        }
    }
}
