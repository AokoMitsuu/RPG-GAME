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
        AppManager.Instance.SaveLoadManager.Save();
        AppManager.Instance.PlayerManager.SetPlayerMovable(true);

        _machine.GetBlackboardVariable<GameObject>("actionBox").SetActive(false);
        _machine.GetBlackboardVariable<GameObject>("fightUIGameObject").SetActive(false);
        
        _machine.SwitchState(_machine.NoneFightState);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
