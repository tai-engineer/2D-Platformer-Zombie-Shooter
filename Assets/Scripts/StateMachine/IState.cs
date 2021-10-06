using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZS
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}
