using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class EntityEndAttackFightState : State
{
    public EntityEndAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        FightAction _fightAction = _machine.GetBlackboardVariable<FightAction>("fightAction");
        
        _fightAction.EntityAction.ResetCharge();
        _fightAction.EntityAction = null;
        _machine.GetBlackboardVariable<GameObject>("actionBox").SetActive(false);
        
        _machine.SetBlackboardVariable("fightAction", _fightAction);
        _machine.SwitchState(_machine.ChargingFightState);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
