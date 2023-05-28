using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class EnemyAttackFightState : State
{
    private Animator _animator;
    private int _damage;
    public EnemyAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _damage = Mathf.Max(1,  _machine.GetBlackboardVariable<EnemyClass>("enemyAction").GetEnemyAttack() - _machine.GetBlackboardVariable<HeroClass>("heroTarget").GetHeroDefense());
        
        _animator = _machine.GetBlackboardVariable<Animator>("attackAnimator");
        
        _animator.gameObject.transform.position = _machine.GetBlackboardVariable<HeroClass>("heroTarget").GO.transform.position;
        _animator.gameObject.SetActive(true);
        _animator.runtimeAnimatorController = _machine.GetBlackboardVariable<EnemyClass>("enemyAction").GetEnemyBaseAttackAnimator();
    }

    protected override void OnUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
        {
            //remove animation
            _animator.gameObject.SetActive(false);
            AppManager.Instance.FightManager.InstantiateDamagePopups(_damage, _machine.GetBlackboardVariable<HeroClass>("heroTarget").GO.transform.position);
            _machine.GetBlackboardVariable<HeroClass>("heroTarget").TakeDamage(_damage);
        
            if (_machine.GetBlackboardVariable<List<HeroClass>>("heroes").Count > 0)
            {
                _machine.SetBlackboardVariable("endPos", _machine.GetBlackboardVariable<Vector3>("enemyActionGameObjectInitialPosition"));
                _machine.SetBlackboardVariable("stateAfterMove", _machine.EnemyEndAttackFightState);
            
                _machine.SwitchState(_machine.SprtieMovingFightState);
            }
        }
    }

    protected override void OnExit()
    {
        
    }
}
