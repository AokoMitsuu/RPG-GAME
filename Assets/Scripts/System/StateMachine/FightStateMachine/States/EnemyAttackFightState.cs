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
        int damage = Mathf.Max(1, _machine.GetBlackboardVariable<EnemyClass>("enemyAction").GetEnemyAttack() - _machine.GetBlackboardVariable<HeroClass>("heroTarget").GetHeroDefense());
        
        AppManager.Instance.FightManager.InstantiateDamagePopups(damage, _machine.GetBlackboardVariable<HeroClass>("heroTarget").GO.transform.position);
        
        _machine.GetBlackboardVariable<HeroClass>("heroTarget").TakeDamage(damage);
        
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
