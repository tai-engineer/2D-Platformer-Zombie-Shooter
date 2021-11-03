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
        void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        Root _ai;
        void OnEnable()
        {
            _ai = new Root();
            _ai.OpenBranch(
                BT.Repeat(5).OpenBranch(
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
        }
    }
}
