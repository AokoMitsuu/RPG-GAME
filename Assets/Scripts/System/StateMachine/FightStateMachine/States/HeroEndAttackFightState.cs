using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class HeroEndAttackFightState : State
{
    public HeroEndAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _machine.GetBlackboardVariable<HeroClass>("heroAction").ResetCharge();
        _machine.SetBlackboardVariable("heroAction",null);
        _machine.GetBlackboardVariable<GameObject>("actionBox").SetActive(false);
        
        _machine.SwitchState(_machine.ChargingFightState);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
