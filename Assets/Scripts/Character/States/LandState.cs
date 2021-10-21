using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class LandState : IState
    {
        readonly int _landParameter = Animator.StringToHash("IsLanding");

        Animator _animator;
        public LandState(Animator animator) => _animator = animator;
        public void OnEnter()
        {
            _animator.SetBool(_landParameter, true);
        }

        public void OnExit()
        {
            _animator.SetBool(_landParameter, false);
        }

        public void Tick() { }
    }
}
