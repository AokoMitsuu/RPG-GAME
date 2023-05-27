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
        int damage = Mathf.Max(1, _machine.GetBlackboardVariable<HeroClass>("heroTarget").GetHeroAttack() - _machine.GetBlackboardVariable<EnemyClass>("enemyAction").GetEnemyDefense());
        
        AppManager.Instance.FightManager.InstantiateDamagePopups(damage, _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").GO.transform.position);
        
        _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").TakeDamage(damage);
        
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
