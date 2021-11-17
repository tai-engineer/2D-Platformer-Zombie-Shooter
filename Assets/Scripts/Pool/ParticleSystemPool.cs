using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class ParticleSystemPool : ObjectPool
    {
        public GameObject Pop(Vector3 position, bool useLocal, float lifeTime)
        {
            GameObject obj = base.Pop(position, useLocal);
            if(obj.TryGetComponent(out ParticleEffect effect))
            {
                effect.lifeTime = lifeTime;
                effect.Enable();
            }

            return obj;
        }
    }
}
