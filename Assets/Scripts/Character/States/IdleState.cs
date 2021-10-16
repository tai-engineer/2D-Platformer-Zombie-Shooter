using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class IdleState : IState
    {
        PlayerController _controller;
        public IdleState(PlayerController controller) => _controller = controller;
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
            _controller.VerticalCollisionCheck(false);
        }
    }
}
