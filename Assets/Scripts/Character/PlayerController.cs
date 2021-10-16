using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    // Handle player physics
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        #region Components
        BoxCollider2D _boxCollider;
        Rigidbody2D _rb2D;
        PlayerCharacter _player;
        Animator _animator;
        #endregion
        #region Serializable Fields
        [SerializeField, Tooltip("Raycast length starting from edge of box collider")]
        float _verticalCheckDistance;
        [SerializeField] float _maxGroundSpeed;
        [SerializeField] float _maxJumpSpeed;
        #endregion
        Vector2 _moveVector;
        static int _moveParameter = Animator.StringToHash("IsMoving");
        static int _sprintParameter = Animator.StringToHash("IsSprinting");
        static int _jumpParameter = Animator.StringToHash("IsJumping");
        void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rb2D = GetComponent<Rigidbody2D>();
            _player = GetComponent<PlayerCharacter>();
            _animator = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            _moveVector = _player.MoveInput * _maxGroundSpeed;
            _animator.SetBool(_moveParameter, _moveVector.x != 0);
            _animator.SetBool(_sprintParameter, false);
            _animator.SetBool(_jumpParameter, false);
            if (_player.SprintInput)
            {
                _moveVector *= 1.5f;
                _animator.SetBool(_sprintParameter, true);
            }
            if(_player.JumpInput)
            {
                SetY(_maxJumpSpeed);
                _animator.SetBool(_jumpParameter, true);
            }
            _rb2D.MovePosition(_rb2D.position + _moveVector * Time.deltaTime);
        }
        void Update()
        {
            VerticalCollisionCheck(true);
            VerticalCollisionCheck(false);
        }
        public bool VerticalCollisionCheck(bool above)
        {
            Vector2 size = _boxCollider.size;
            Vector2 center = _boxCollider.bounds.center;
            Vector2 direction = above ? Vector2.up : Vector2.down;
            float offset = 0.1f;
            Vector2 middle = center + direction * (size.y * 0.5f + offset);

            Vector2[] raycast = new Vector2[3];
            raycast[0] = middle + Vector2.left * size.x * 0.5f;
            raycast[1] = middle;
            raycast[2] = middle + Vector2.right * size.x * 0.5f;
            RaycastHit2D[] hits = new RaycastHit2D[3];
            Vector2[] normals = new Vector2[3];
            Vector2 result = Vector2.zero;
            for(int i = 0; i < raycast.Length; i++)
            {
                hits[i] = Physics2D.Raycast(raycast[i], direction, _verticalCheckDistance);
                normals[i] = hits[i].collider ? hits[i].normal : Vector2.zero;
                result += normals[i];
            }
            return Mathf.Approximately(result.y, 3f);
        }
        public void SetX(float value)
        {
            _moveVector.x = value;
        }
        public void SetY(float value)
        {
            _moveVector.y = value;
        }
    }
}
