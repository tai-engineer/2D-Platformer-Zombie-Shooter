using UnityEngine;
using UnityEngine.Events;
using System;
namespace PZS
{
    public class Damager : MonoBehaviour
    {
        [Serializable]
        public class DamageableEvent: UnityEvent<Damager, Damageable> { }


        public int damage = 1;
        public Vector2 offset;
        public Vector2 size = new Vector2(1f,1f);
        public bool canHitTrigger;
        public bool disableDamageAfterHit = true;
        public LayerMask hittableLayer;
        public DamageableEvent onDamageableHit;


        ContactFilter2D _contactFilter;
        Collider2D[] _attackOverlapResults = new Collider2D[6];
        bool _canDamage = true;
        void Awake()
        {
            _contactFilter.layerMask = hittableLayer;
            _contactFilter.useLayerMask = true;
            _contactFilter.useTriggers = canHitTrigger;
        }

        void FixedUpdate()
        {

            if (!_canDamage)
                return;

            Vector2 scale = transform.lossyScale;
            Vector2 facingOffset = Vector2.Scale(offset, scale);
            Vector2 scaledSize = Vector2.Scale(scale, size);

            Vector2 pointA = (Vector2)transform.position + facingOffset - scaledSize * 0.5f;
            Vector2 pointB = pointA + scaledSize;

            int hitCount = Physics2D.OverlapArea(pointA, pointB, _contactFilter, _attackOverlapResults);
            for (int i = 0; i < hitCount; i++)
            {
                if(_attackOverlapResults[i].TryGetComponent<Damageable>(out Damageable damageable))
                {
                    damageable.TakeDamage(this);
                    onDamageableHit.Invoke(this, damageable);
                    if (disableDamageAfterHit)
                        DisableDamage();
                }
            }
        }

        public void DisableDamage()
        {
            _canDamage = false;
        }

        public void EnableDamage()
        {
            _canDamage = true;
        }
    }
}
