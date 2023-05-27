using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class HeroAttackFightState : State
{
    public HeroAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").TakeDamage(_machine.GetBlackboardVariable<HeroClass>("heroAction").GetHeroAttack());
        
        if (_machine.GetBlackboardVariable<List<EnemyClass>>("enemies").Count > 0)
        {
            _machine.SetBlackboardVariable("endPos", _machine.GetBlackboardVariable<Vector3>("heroActionGameObjectInitialPosition"));
            _machine.SetBlackboardVariable("stateAfterMove", _machine.HeroEndAttackFightState);
                
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
