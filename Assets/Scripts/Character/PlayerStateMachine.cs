using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PZS
{
    [RequireComponent(typeof(PlayerCharacter), typeof(Animator))]
    public class PlayerStateMachine : MonoBehaviour
    {
        StateMachine _stateMachine;
        PlayerController _controller;
        Animator _animator;
        PlayerCharacter _player;
        void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
            _player = GetComponent<PlayerCharacter>();

            _stateMachine = new StateMachine();
            
            var _idleState = new IdleState(_controller);
            var _runState = new MoveState(_controller, _animator);
            var _climpState = new ClimpState();
            var _jumpState = new JumpState(_controller, _animator, _controller.MaxJumpSpeed);
            var _fallState = new FallState(_controller, _animator);
            var _landState = new LandState(_animator);


            At(_idleState, _runState, IsMoving);
            At(_idleState, _jumpState, IsJumping);
            
            At(_runState, _idleState, IsStopMoving);

            At(_jumpState, _fallState, IsFalling);
            At(_fallState, _landState, IsGrounded);
            At(_landState, _idleState, IsLanded);

            _stateMachine.SetState(_idleState);
        }
        void Update()
        {
            _stateMachine.Tick();
        }

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
        void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        bool IsMoving() => !Mathf.Approximately(_player.MoveInput.x, 0f);
        bool IsStopMoving() => Mathf.Approximately(_player.MoveInput.x, 0f);
        bool IsJumping() => _player.JumpInput;
        bool IsGrounded() => _controller.VerticalCollisionCheck(false);
        bool IsFalling() => _controller.MoveVector.y < 0f;
        bool IsLanded() => _controller.IsLanded;
    }
}
