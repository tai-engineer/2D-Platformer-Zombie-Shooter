using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PZS
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] InputReaderSO _input;
        StateMachine _stateMachine;
        public Vector3 MoveInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool JumpInput { get; private set; }
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
        void OnJump(bool jumpInput)
        {
            JumpInput = jumpInput;
        }
        void OnSprint(bool sprintInput)
        {
            SprintInput = sprintInput;
        }
        void OnMove(Vector2 moveInput)
        {
            MoveInput = moveInput;
        }
        #endregion
    }
}
