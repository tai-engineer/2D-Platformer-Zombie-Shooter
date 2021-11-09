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
            return base.Pop(position, useLocal);
        }
    }
}
