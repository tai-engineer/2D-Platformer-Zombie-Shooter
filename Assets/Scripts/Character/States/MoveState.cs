using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class MoveState : IState
    {
        public MoveState() { }
        public void OnEnter()
        {
            Debug.Log($"Enter {this.GetType()}");
        }

        public void OnExit()
        {
            Debug.Log($"Exit {this.GetType()}");
        }

        public void Tick()
        {
            Debug.Log($"Tick {this.GetType()}");
        }
    }
}
