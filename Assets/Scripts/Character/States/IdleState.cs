using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class IdleState : IState
    {
        public IdleState() { }
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
