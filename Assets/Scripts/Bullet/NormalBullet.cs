using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class NormalBullet : MonoBehaviour
    {
        [SerializeField] float _speed;
        void Update()
        {
            var dir = transform.right;
            transform.position += dir * _speed * Time.deltaTime;
        }
    }
}
