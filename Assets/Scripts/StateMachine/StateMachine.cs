using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PZS
{
    public class StateMachine
    {
        IState _currentState;

        Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        public void Tick()
        {
            if(_transitions.TryGetValue(_currentState.GetType(), out List<Transition> transitions))
            {
                var transition = GetTransition(transitions);
                if (transition != null)
                {
                    SetState(transition.To); 
                }
            }
            _currentState.Tick();
        }

        public void SetState(IState state)
        {
            //To avoid calling this function many times in same state
            if (_currentState == state)
            {
                return;
            }
            _currentState?.OnExit();
            _currentState = state;
            _currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            transitions.Add(new Transition(to, condition));
        }
        Transition GetTransition(List<Transition> transitions)
        {
            foreach(var transition in transitions)
            {
                if (transition.Condition())
                    return transition;
            }
            return null;
        }
        private class Transition
        {
            public Func<bool> Condition { get; }
            public IState To { get; }
            public Transition(IState to, Func<bool> condition)
            {
                To = to;
                Condition = condition;
            }
        }
    }
}
