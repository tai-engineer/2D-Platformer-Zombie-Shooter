using UnityEngine;
using UnityEngine.Events;
using System;
namespace PZS
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField] SharedInt _currentHealth;

        [Serializable]
        public class DamageEvent: UnityEvent<Damager, Damageable> { }


        public Damageable OnTakeDamage;
        public void TakeDamage(Damager damager)
        {
            _currentHealth.Value -= damager.damage;
        }
    }
}
