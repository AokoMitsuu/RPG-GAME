using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class WaitActionFightState : State
{
    public WaitActionFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _machine.GetBlackboardVariable<GameObject>("actionBox").SetActive(true);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
