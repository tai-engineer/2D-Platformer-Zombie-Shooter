using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZS.BTAI;

namespace PZS
{
    [RequireComponent(typeof(PlayerController))]
    public class BossBehaviour : MonoBehaviour
    {
        PlayerController _controller;
        Animator _animator;
        void Awake()
        {
            _controller = GetComponent<PlayerController>();
            _animator = GetComponent<Animator>();
        }

        Root _ai;
        void OnEnable()
        {
            _ai = new Root();
            _ai.OpenBranch(
                BT.Repeat(3).OpenBranch(
                    BT.Call(Test1),
                    BT.Call(Test2),
                    BT.Call(Test3)
                    ),
                BT.Terminate()
                );
        }

        void Update()
        {
            _ai.Tick();
        }
        bool SeekForPlayer()
        {
            return false;
        }

        void Test1()
        {
            Debug.Log("1");
        }
        void Test2()
        {
            Debug.Log("2");
        }
        void Test3()
        {
            Debug.Log("3");
        }
    }
}
