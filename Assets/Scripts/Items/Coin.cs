using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Coin : MonoBehaviour
    {
        enum MoveDirection { Random, Specific}
        [SerializeField] bool _hasMovement;
        [SerializeField] float _speed;
        [SerializeField] MoveDirection _moveType;
        [Tooltip("Only use when object has Specific movement type")]
        [SerializeField] Vector2 _moveVector;
        [SerializeField] int _gold;

        Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        void OnEnable()
        {
            if (!_hasMovement)
                return;
            if (_moveType == MoveDirection.Random)
            {
                var x = Random.Range(-0.5f, 0.5f);
                var y = Random.Range(0.3f, 0.6f);
                _moveVector = new Vector2(x, y); 
            }
            _rb.AddForce(_moveVector * _speed, ForceMode2D.Impulse);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                PlayerCharacter character = collision.gameObject.GetComponent<PlayerCharacter>();
                character.IncreaseGold(_gold);
                gameObject.SetActive(false);
            }
        }
    }
}
