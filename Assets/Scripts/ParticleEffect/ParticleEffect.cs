using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class ParticleEffect : MonoBehaviour
    {
        public float lifeTime = 1f;
        ParticleSystemPool _pool;

        public void Enable()
        {
            if (_pool == null)
            {
                _pool = PoolManager.GetPool<ParticleSystemPool>(); 
            }

            StartCoroutine(DisableAfterLifetime(lifeTime));
        }
        IEnumerator DisableAfterLifetime(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            _pool.Return(gameObject);
        }
    }
}
