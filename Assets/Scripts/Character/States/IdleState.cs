using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public class IdleState : IState
    {
        CharacterController _controller;
        public IdleState(CharacterController controller) => _controller = controller;
        public void OnEnter()
        {
            _controller.ResetMoveVector();
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }
    }
}
