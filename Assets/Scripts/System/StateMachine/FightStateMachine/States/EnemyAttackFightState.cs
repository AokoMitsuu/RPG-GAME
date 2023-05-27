using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class EnemyAttackFightState : State
{
    public EnemyAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _machine.GetBlackboardVariable<HeroClass>("heroTarget").TakeDamage(_machine.GetBlackboardVariable<EnemyClass>("enemyAction").GetEnemyAttack());
        
        if (_machine.GetBlackboardVariable<List<HeroClass>>("heroes").Count > 0)
        {
            _machine.SetBlackboardVariable("endPos", _machine.GetBlackboardVariable<Vector3>("enemyActionGameObjectInitialPosition"));
            _machine.SetBlackboardVariable("stateAfterMove", _machine.EnemyEndAttackFightState);
            
            _machine.SwitchState(_machine.SprtieMovingFightState);
        }
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
