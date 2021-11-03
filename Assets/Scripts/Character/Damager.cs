using UnityEngine;
using UnityEngine.Events;
using System;
namespace PZS
{
    public class Damager : MonoBehaviour
    {
        [Serializable]
        public class DamagableEvent: UnityEvent<Damager, Damageable> { }
        public int damage;
    }
}
