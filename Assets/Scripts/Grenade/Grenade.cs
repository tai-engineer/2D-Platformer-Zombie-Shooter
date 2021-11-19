using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Grenade : MonoBehaviour
    {
        [SerializeField] GameObject _impactFX;

        Rigidbody2D _rb2D;
        Vector2 _target = Vector3.zero;
        float _angle = 45f;
        Vector2 _direction = Vector2.zero;
        float _impactDelay = 1.0f;
        void Awake()
        {
            _rb2D = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// V0x = sqrt(G * R * R / 2 * (H - R * tan(alpha)))
        /// V0y = V0x * tan(alpha)
        /// </summary>
        float _tanAlpha;
        float _distance;

        // height = _target.y - transform.position.y;
        float _height = 0f; // Ignore target height
        Vector2 GetProjectileVelocity()
        {
            float G = Physics2D.gravity.y * _rb2D.gravityScale;

            float Vx = Mathf.Sqrt(G * _distance * _distance / (2f * (_height - _distance * _tanAlpha)));
            float Vy = Vx * _tanAlpha;
            return new Vector2(Vx * _direction.x, Vy * _direction.y);
        }
        public void SetTargetRange(float xRange, float yRange)
        {
            _target.y = yRange;
            _target.x = transform.position.x + xRange;
            _distance = Vector2.Distance(transform.position, _target);
        }
        public void SetAngle(float angle)
        {
            _angle = angle;
            _tanAlpha = Mathf.Tan(_angle * Mathf.Deg2Rad);
        }
        public void SetDirection(float xDir, float yDir)
        {
            _direction.x = xDir;
            _direction.y = yDir;
        }
        public void Launch(float range, float angle, Vector2 dir)
        {
            if (!Mathf.Approximately(_angle, angle))
            {
                SetAngle(angle); 
            }

            if (!Mathf.Approximately(_direction.x, dir.x))
            {
                SetDirection(dir.x, 1f); // left or right 
            }

            SetTargetRange(range, transform.position.y); // Ignore target height
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
