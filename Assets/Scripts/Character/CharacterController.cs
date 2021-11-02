using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    // Handle player physics
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        #region Components
        BoxCollider2D _boxCollider;
        Rigidbody2D _rb2D;
        #endregion
        #region Serializable Fields
        [SerializeField, Tooltip("Raycast length starting from edge of box collider")]
        float _verticalCheckDistance;
        [SerializeField] float _maxGroundSpeed;
        [SerializeField] float _maxJumpSpeed;
        [SerializeField] Transform _shootPosition;
        [SerializeField] ObjectPool _bulletPool;

        #endregion
        Vector2 _moveVector;
        public float Gravity { get; private set; }
        public Vector2 MoveVector { get { return _moveVector; } }
        public float MaxJumpSpeed { get { return _maxJumpSpeed; } }
        public bool IsLanded { get; private set; }
        void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rb2D = GetComponent<Rigidbody2D>();

            Gravity = Physics2D.gravity.y;
        }

        void FixedUpdate()
        {
            Vector2 movement = _moveVector * Time.deltaTime;
            _rb2D.MovePosition(_rb2D.position + movement);
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
        public void SetHorizontalMovement(float value)
        {
            _moveVector.x = value;
        }
        public void SetVerticalMovement(float value)
        {
            _moveVector.y = value;
        }
        public void AddVerticalMovement(float value)
        {
            _moveVector.y += value;
        }
        public void SetMoveVector(Vector2 value)
        {
            _moveVector = value;
        }
        public void ResetMoveVector()
        {
            SetMoveVector(Vector2.zero);
        }
        public void HorizontalMove(float input)
        {
            SetHorizontalMovement(input * _maxGroundSpeed);
        }
        public void VerticalMove()
        {
            AddVerticalMovement(Gravity * Time.deltaTime);
        }
        public void LandingPrepare()
        {
            IsLanded = false;
        }
        public void LandingEnd()
        {
            IsLanded = true;
        }
        public void Shoot()
        {
            _bulletPool.Pop(_shootPosition.position, false);
        }
    }
}
