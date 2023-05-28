using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEditor.Animations;

public class HeroAttackFightState : State
{
    private Animator _animator;
    private FightAction _action;
    private int _damage;
    public HeroAttackFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _animator = _machine.GetBlackboardVariable<Animator>("attackAnimator");
        _action = _machine.GetBlackboardVariable<FightAction>("action");
        
        _damage = Mathf.Max(1,  _action.Damage - _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").GetEnemyDefense());
        
        _animator.gameObject.transform.position = _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").GO.transform.position;
        _animator.gameObject.SetActive(true);
        _animator.runtimeAnimatorController = _action.AnimatorController;
    }

    protected override void OnUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
        {
            //remove animation
            _animator.gameObject.SetActive(false);
            AppManager.Instance.FightManager.InstantiateDamagePopups(_damage, _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").GO.transform.position);
            _machine.GetBlackboardVariable<EnemyClass>("enemyTarget").TakeDamage(_damage);
        
            if (_machine.GetBlackboardVariable<List<EnemyClass>>("enemies").Count > 0)
            {
                _machine.SetBlackboardVariable("endPos", _machine.GetBlackboardVariable<Vector3>("heroActionGameObjectInitialPosition"));
                _machine.SetBlackboardVariable("stateAfterMove", _machine.HeroEndAttackFightState);
                
                _machine.SwitchState(_machine.SprtieMovingFightState);
            }
        }
    }

    protected override void OnExit()
    {
        
    }
}
