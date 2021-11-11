using UnityEngine;
using UnityEngine.Events;
using System;
namespace PZS
{
    public class Damageable : MonoBehaviour
    {
        [Serializable]
        public class DamageEvent: UnityEvent<Damager, Damageable> { }

        public DamageEvent OnTakeDamage;
        public void TakeDamage(Damager damager)
        {
            OnTakeDamage.Invoke(damager, this);
        }
    }
}
