using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class NormalBullet : MonoBehaviour
    {
        [SerializeField] float _speed;

        Damager _damager;
        BulletPool _bulletPool;
        Vector2 _direction;
        Rigidbody2D _rb2D;
        void Awake()
        {
            _damager = GetComponent<Damager>();
            _rb2D = GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            _bulletPool = PoolManager.GetPool<BulletPool>();

            _damager.onDamageableHit.AddListener(OnDamageableHit);
        }
        void FixedUpdate()
        {
            _rb2D.MovePosition(_rb2D.position + _direction * _speed * Time.deltaTime);
        }

        void OnDamageableHit(Damager damager, Damageable damageable)
        {
            _bulletPool.Return(damager.gameObject);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
