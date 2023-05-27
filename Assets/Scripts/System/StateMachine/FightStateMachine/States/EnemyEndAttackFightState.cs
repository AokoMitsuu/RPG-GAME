using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class EnemyEndAttackFightState : State
{
    public EnemyEndAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _machine.GetBlackboardVariable<EnemyClass>("enemyAction").ResetCharge();
        _machine.SetBlackboardVariable("_enemyAction", null);
        _machine.SwitchState(_machine.ChargingFightState);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
