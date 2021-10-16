using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PZS
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] InputReaderSO _input;

        static int _jumpParameter = Animator.StringToHash("IsJumping");

        public Vector3 MoveInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool JumpInput { get; private set; }
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
