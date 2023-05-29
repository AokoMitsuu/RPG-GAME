using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class ChargingFightState : State
{
    private List<EntityClass> _heroes;
    private List<EntityClass> _enemies;
    
    private GameObject _heroActionGameObject;
    private GameObject _enemyActionGameObject;
    
    private FightPlayerPreview _fightPlayerAction;
    
    private HeroClass _heroAction;
    private EnemyClass _enemyAction;
    
    private Vector3 _heroActionGameObjectInitialPosition;
    private Vector3 _enemyActionGameObjectInitialPosition;
    
    private HeroClass _heroTarget;

    private int _chargeTo;
    private FightAction _fightAction;
    public ChargingFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _heroes = _machine.GetBlackboardVariable<List<EntityClass>>("heroes");
        _enemies = _machine.GetBlackboardVariable<List<EntityClass>>("enemies");
        
        _fightPlayerAction = _machine.GetBlackboardVariable<FightPlayerPreview>("fightPlayerAction");
        
        _chargeTo = (int)_machine.GetBlackboardVariable<float>("chargeMaxSecond") * _machine.GetBlackboardVariable<int>("charge");
        
        _fightAction = _machine.GetBlackboardVariable<FightAction>("fightAction");
    }

    protected override void OnUpdate()
    {
        for (int i = 0; i < _heroes.Count; i++)
        {
            if (_heroes[i].ChargeAction(_chargeTo))
            {
                _heroActionGameObject = _heroes[i].GO;
                _fightPlayerAction.InitPlayerPreviewAction((HeroClass)_heroes[i]);
                _heroAction = (HeroClass)_heroes[i];
                _heroActionGameObjectInitialPosition = _heroActionGameObject.transform.position;

                
                _fightAction.EntityAction = _heroAction;
                _fightAction.EntityToMove = _heroActionGameObject;
                _fightAction.TargetPos = _heroActionGameObject.transform.position + new Vector3(75, 0, 0);
                _fightAction.EntityInitalPos = _heroActionGameObjectInitialPosition;
                
                _machine.SetBlackboardVariable("stateAfterMove", _machine.WaitActionFightState);
                _machine.SetBlackboardVariable("fightAction", _fightAction);
                
                AppManager.Instance.FightManager.UpdateSkillPanel();
                
                _machine.SwitchState(_machine.SprtieMovingFightState);
                return;
            }
        }
        
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].ChargeAction(_chargeTo))
            {
                _enemyActionGameObject = _enemies[i].GO;
                _enemyActionGameObjectInitialPosition = _enemyActionGameObject.transform.position;
                _enemyAction = (EnemyClass)_enemies[i];
                
                int randomHero = Random.Range(0, _heroes.Count);
                _heroTarget = (HeroClass)_heroes[randomHero];

                _fightAction.EntityTarget = _heroTarget;
                _fightAction.EntityAction = _enemyAction;
                _fightAction.EntityToMove = _enemyActionGameObject;
                _fightAction.TargetPos = _heroTarget.GO.transform.position + new Vector3(150,0,0);
                _fightAction.EntityInitalPos = _enemyActionGameObjectInitialPosition;
                _fightAction.AnimatorController = _enemyAction.GetBaseAttackAnimatorController();
                _fightAction.Damage = _enemyAction.GetAttack();
                _fightAction.FightActionType = FightActionType.Attack;
                
                _machine.SetBlackboardVariable("stateAfterMove", _machine.EntityActionFightState);
                _machine.SetBlackboardVariable("fightAction", _fightAction);
                _machine.SwitchState(_machine.SprtieMovingFightState);
                return;
            }
        }
    }

    protected override void OnExit()
    {
        
    }
}
