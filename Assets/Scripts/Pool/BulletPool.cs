using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PZS
{
    public class BulletPool : ObjectPool
    {
        public override GameObject Pop(Vector3 position, bool useLocal)
        {
            GameObject obj = base.Pop(position, useLocal);
            if(obj.TryGetComponent(out Damager damager))
            {
                damager.EnableDamage();
            }
            return obj;
        }

        public GameObject Pop(Vector3 position, Vector2 direction, bool useLocal)
        {
            GameObject obj = Pop(position, useLocal);
            if (obj.TryGetComponent(out NormalBullet normalBullet))
            {
                normalBullet.SetDirection(direction);
            }
            return obj;
        }
    }
}
