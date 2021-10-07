using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PZS
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] InputReaderSO _input;
        [SerializeField] PlayerController _playerPhysic;
        StateMachine _stateMachine;
        void Awake()
        {
            _stateMachine = new StateMachine();
            var _idleState = new IdleState();
            var _runState = new MoveState();
            var _climpState = new ClimpState();

            _stateMachine.SetState(_idleState);
        }

        void Update()
        {
            _stateMachine.Tick();
        }
        void OnEnable()
        {
            if(_input)
            {
                _input.shootEvent  += OnShoot;
                _input.crouchEvent += OnCrouch;
                _input.jumpEvent   += OnJump;
                _input.sprintEvent += OnSprint;
                _input.moveEvent   += OnMove;
            }
        }

        void OnDisable()
        {
            if (_input)
            {
                _input.shootEvent  -= OnShoot;
                _input.crouchEvent -= OnCrouch;
                _input.jumpEvent   -= OnJump;
                _input.sprintEvent -= OnSprint;
                _input.moveEvent   -= OnMove;
            }
        }

        void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
        #region Input
        void OnShoot()
        {
            Debug.Log("Shooting");
        }
        void OnCrouch()
        {
            Debug.Log("Crouching");
        }
        void OnJump()
        {
            Debug.Log("Jumping");
        }
        void OnSprint()
        {
            Debug.Log("Sprinting");
        }
        void OnMove(Vector2 moveInput)
        {
            Debug.Log("moveInput = " + moveInput);
        }
        #endregion
    }
}
