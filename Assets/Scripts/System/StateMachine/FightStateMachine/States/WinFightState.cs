using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class WinFightState : State
{
    public WinFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        int xp = _machine.GetBlackboardVariable<int>("totalXp");
        List<EntityClass> heroes = _machine.GetBlackboardVariable<List<EntityClass>>("heroes");
        foreach (EntityClass hero in heroes)
        {
            ((HeroClass)hero).AddXp(xp/4);
        }
        
        AppManager.Instance.SaveLoadManager.Save();
        AppManager.Instance.PlayerManager.SetPlayerInteractable(true);
        
        _machine.GetBlackboardVariable<GameObject>("actionBox").SetActive(false);
        _machine.GetBlackboardVariable<GameObject>("fightUIGameObject").SetActive(false);
        AppManager.Instance.FightManager.ClearPopups();
        
        _machine.SwitchState(_machine.NoneFightState);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
