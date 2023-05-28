using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class ChargingFightState : State
{
    private List<HeroClass> _heroes;
    private List<EnemyClass> _enemies;
    
    private GameObject _heroActionGameObject;
    private GameObject _enemyActionGameObject;
    
    private FightPlayerPreview _fightPlayerAction;
    
    private HeroClass _heroAction;
    private EnemyClass _enemyAction;
    
    private Vector3 _heroActionGameObjectInitialPosition;
    private Vector3 _enemyActionGameObjectInitialPosition;
    
    private HeroClass _heroTarget;

    private int _chargeTo;
    public ChargingFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _heroes = _machine.GetBlackboardVariable<List<HeroClass>>("heroes");
        _enemies = _machine.GetBlackboardVariable<List<EnemyClass>>("enemies");
        
        _fightPlayerAction = _machine.GetBlackboardVariable<FightPlayerPreview>("fightPlayerAction");
        
        _chargeTo = 3 * _machine.GetBlackboardVariable<int>("Charge");
    }

    protected override void OnUpdate()
    {
        for (int i = 0; i < _heroes.Count; i++)
        {
            if (_heroes[i].ChargeAction(_chargeTo))
            {
                _heroActionGameObject = _heroes[i].GO;
                _fightPlayerAction.InitPlayerPreviewAction(_heroes[i]);
                _heroAction = _heroes[i];
                _heroActionGameObjectInitialPosition = _heroActionGameObject.transform.position;
                
                _machine.SetBlackboardVariable("heroAction", _heroAction);
                _machine.SetBlackboardVariable("entityToMove", _heroActionGameObject);
                _machine.SetBlackboardVariable("endPos", _heroActionGameObject.transform.position + new Vector3(75,0,0));
                _machine.SetBlackboardVariable("stateAfterMove", _machine.WaitActionFightState);
                _machine.SetBlackboardVariable("heroActionGameObjectInitialPosition", _heroActionGameObjectInitialPosition);
                
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
                _enemyAction = _enemies[i];
                
                int randomHero = Random.Range(0, _heroes.Count);
                _heroTarget = _heroes[randomHero];
                
                _machine.SetBlackboardVariable("heroTarget", _heroTarget);
                _machine.SetBlackboardVariable("enemyAction", _enemyAction);
                _machine.SetBlackboardVariable("entityToMove", _enemyActionGameObject);
                _machine.SetBlackboardVariable("endPos", _heroTarget.GO.transform.position + new Vector3(150,0,0));
                _machine.SetBlackboardVariable("stateAfterMove", _machine.EnemyAttackFightState);
                _machine.SetBlackboardVariable("enemyActionGameObjectInitialPosition", _enemyActionGameObjectInitialPosition);
                
                _machine.SwitchState(_machine.SprtieMovingFightState);
                return;
            }
        }
    }

    protected override void OnExit()
    {
        
    }
}
