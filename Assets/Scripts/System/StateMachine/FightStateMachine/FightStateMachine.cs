using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class FightStateMachine
{
    public State NoneFightState { get; private set; }
    public State ChargingFightState { get; private set; }
    public State EntityActionFightState { get; private set; }
    public State EntityEndActionFightState { get; private set; }
    public State WaitActionFightState { get; private set; }
    public State WaitTargetFightState { get; private set; }
    public State SprtieMovingFightState { get; private set; }
    public State WinFightState { get; private set; }
    public State LostFightState { get; private set; }

    public State CurrentState { get; private set; }

    private State _startingState ;
    
    private Dictionary<string, object> _blackboard = new();

    public FightStateMachine()
    {
        Init();
    }

    public void Init()
    {
        NoneFightState = new NoneFightState(this);
        
        ChargingFightState = new ChargingFightState(this);

        EntityActionFightState = new EntityActionFightState(this);
        EntityEndActionFightState = new EntityEndActionFightState(this);
        
        WaitActionFightState = new WaitActionFightState(this);
        WaitTargetFightState = new WaitTargetFightState(this);
        SprtieMovingFightState = new SpriteMovingFightState(this);
        
        WinFightState = new WinFightState(this);
        LostFightState = new LostFightState(this);

        _startingState = NoneFightState;
        SwitchState(_startingState);
    }
    
    public void SwitchState(State state)
    {
        CurrentState?.Exit();

        CurrentState = state ?? _startingState;
        CurrentState?.Enter();
    }
    
    public void UpdateMachine()
    {
        CurrentState?.Update();
    }

    // ================================ BLACKBOARD ======================================

    public void SetBlackboardVariable(string variableName, object variable)
    {
        _blackboard[variableName] = variable;
    }

    public T GetBlackboardVariable<T>(string variableName)
    {
        if (_blackboard.TryGetValue(variableName, out object value))
        {
            return (T)value;
        }
        else
        {
            return default;
        }
    }
}
