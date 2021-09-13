using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] InputReaderSO _input;

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
    }
}
