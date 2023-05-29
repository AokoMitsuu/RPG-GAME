using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class WaitTargetFightState : State
{
    private List<EntityClass> _entitiesTargets;
    
    public WaitTargetFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _entitiesTargets = _machine.GetBlackboardVariable<List<EntityClass>>("entitiesTarget");
        _machine.GetBlackboardVariable<GameObject>("cancelTargetPanel").SetActive(true);
        
        foreach (EntityClass entity in _entitiesTargets)
        {
            entity.EntityFightUI.ToggleTargetPanel(true);
        }
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnExit()
    {
        _entitiesTargets = _machine.GetBlackboardVariable<List<EntityClass>>("entitiesTarget");
        _machine.GetBlackboardVariable<GameObject>("cancelTargetPanel").SetActive(false);
        
        foreach (EntityClass entity in _entitiesTargets)
        {
            entity.EntityFightUI.ToggleTargetPanel(false);
        }
    }
}
