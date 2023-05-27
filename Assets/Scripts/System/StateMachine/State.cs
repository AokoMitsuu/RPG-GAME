using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class State
    {
        protected FightStateMachine _machine;
        
        private Coroutine _stateCoroutine;
        
        public State(FightStateMachine machine)
        {
            _machine = machine;
        }
        
        public void Enter()
        {
            OnEnter();
        }
        
        public void Update() => OnUpdate();

        public void Exit()
        {
            OnExit();
            EndState();
        }
        
        protected void EndState()
        {
            if (_stateCoroutine != null)
            {
                _stateCoroutine = null;
            }
        }
        
        protected virtual void OnEnter() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnExit() { }
    }
}

