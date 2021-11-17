using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class NormalBullet : MonoBehaviour
    {
        [SerializeField] GameObject _impactFX;
        [SerializeField] float _speed;
        [SerializeField] float _explosionDelay = 0.1f;

        BulletPool _bulletPool;
        ParticleSystemPool _particlePool;
        Vector2 _direction;
        Rigidbody2D _rb2D;

        void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            _bulletPool = PoolManager.GetPool<BulletPool>();
            _particlePool = PoolManager.GetPool<ParticleSystemPool>();
        }
        void FixedUpdate()
        {
            _rb2D.MovePosition(_rb2D.position + _direction * _speed * Time.deltaTime);
        }

        public void OnTargetHit()
        {
            _particlePool.Pop(transform.position, false, _explosionDelay);
            _bulletPool.Return(gameObject);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
