using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Grenade : MonoBehaviour
    {
        [SerializeField] GameObject _impactFX;
        [SerializeField] float _speed;

        Rigidbody2D _rb2D;
        Vector2 _target;
        float _range = 0f;
        float _angle = 45f;
        float _impactDelay = 1.0f;
        void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
            _target = new Vector3(_range, transform.position.y);
        }

        /// <summary>
        /// V0x = sqrt(G * R * R / 2 * (H - R * tan(alpha)))
        /// V0y = V0x * tan(alpha)
        /// </summary>
        Vector2 GetProjectileVelocity()
        {
            float R = Vector2.Distance(transform.position, _target);
            R = Mathf.Min(R, _range);
            float G = Physics2D.gravity.y * _rb2D.gravityScale * _speed;
            float tanAlpha = Mathf.Tan(_angle * Mathf.Deg2Rad);
            float H = _target.y - transform.position.y;

            float Vx = Mathf.Sqrt(G * R * R / (2f * (H - R * tanAlpha)));
            float Vy = Vx * tanAlpha;
            return new Vector2(Vx, Vy);
        }
        public void SetRange(float range) => _range = range;
        public void SetAngle(float angle) => _angle = angle;
        public void Launch(float range, float angle)
        {
            SetAngle(angle);
            SetRange(range);
            _rb2D.AddForce(GetProjectileVelocity(), ForceMode2D.Impulse);
        }
        public void OnExplosion()
        {
            var impactPE = Instantiate(_impactFX, transform.position, Quaternion.identity);
            Destroy(impactPE, _impactDelay);
            Destroy(gameObject);
        }

    }
}
