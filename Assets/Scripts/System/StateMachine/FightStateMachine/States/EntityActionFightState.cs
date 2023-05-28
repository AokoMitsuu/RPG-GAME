using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class EntityActionFightState : State
{
    private Animator _animator;
    private int _damage;
    private FightAction _fightAction;
    public EntityActionFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _fightAction = _machine.GetBlackboardVariable<FightAction>("fightAction");
        
        if (_fightAction.FightActionType == FightActionType.Attack)
        { 
            _damage = Mathf.Max(1,  _fightAction.Damage - _fightAction.EntityTarget.GetDefense());
        }
        
        _animator = _machine.GetBlackboardVariable<Animator>("attackAnimator");
        
        _animator.gameObject.transform.position = _fightAction.EntityTarget.GO.transform.position;
        _animator.gameObject.SetActive(true);
        _animator.runtimeAnimatorController = _fightAction.AnimatorController;
    }

    protected override void OnUpdate()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0))
        {
            _animator.gameObject.SetActive(false);

            switch (_fightAction.FightActionType)
            {
                case FightActionType.Attack:
                    AppManager.Instance.FightManager.InstantiateDamagePopups(_damage, _fightAction.EntityTarget.GO.transform.position);
                    _fightAction.EntityTarget.TakeDamage(_damage);
                    break;
                case FightActionType.Heal:
                    ((HealItemClass)_fightAction.Item).UseEffect((HeroClass)_fightAction.EntityTarget);
                    break;
                case FightActionType.Revive:
                    ((ReviveItemClass)_fightAction.Item).UseEffect((HeroClass)_fightAction.EntityTarget);
                    break;
            }

            if (_machine.GetBlackboardVariable<List<EntityClass>>("heroes").Count > 0 && _machine.GetBlackboardVariable<List<EntityClass>>("enemies").Count > 0)
            {
                _machine.SetBlackboardVariable("endPos", _machine.GetBlackboardVariable<Vector3>("enemyActionGameObjectInitialPosition"));
                _fightAction.TargetPos = _fightAction.EntityInitalPos;
                
                
                _machine.SetBlackboardVariable("stateAfterMove", _machine.EntityEndActionFightState);
                _machine.SetBlackboardVariable("fightAction", _fightAction);
            
                _machine.SwitchState(_machine.SprtieMovingFightState);
            }
        }
    }

    protected override void OnExit()
    {
        
    }
}
