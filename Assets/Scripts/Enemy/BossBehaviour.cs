using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZS.BTAI;

namespace PZS
{
    [RequireComponent(typeof(CharacterController))]
    public class BossBehaviour : MonoBehaviour
    {
        CharacterController _controller;
        Animator _animator;

        int _bulletHitCount = 0;
        [SerializeField] int _startAmmo;
        int _currentAmmo;
        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _currentAmmo = _startAmmo;
        }

        Root _ai;
        void OnEnable()
        {
            _ai = new Root();
            _ai.OpenBranch(
                BT.Repeat(_startAmmo).OpenBranch(
                    BT.Call(Shoot),
                    BT.SetBool(_animator, "Shoot", true),
                    BT.Wait(2),
                    BT.SetBool(_animator, "Shoot", false)
                    ),
                BT.Terminate()
                );
        }

        void Update()
        {
            _ai.Tick();
        }

        void Shoot()
        {
            _controller.Shoot();
            if(_currentAmmo > 0)
                _currentAmmo--;
        }

        void Melee()
        {
            if((_startAmmo - _currentAmmo) == 3 && _bulletHitCount != 3)
            {

            }
        }
        void OnBulletHit()
        {
            _bulletHitCount++;
        }
    }
}
