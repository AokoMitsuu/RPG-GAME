using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class LostFightState : State
{
    public LostFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        System.Action callback = () =>
        {
            _machine.GetBlackboardVariable<GameObject>("actionBox").SetActive(false);
            _machine.GetBlackboardVariable<GameObject>("fightUIGameObject").SetActive(false);
            
            AppManager.Instance.CameraManager.SetCameraTo(AppManager.Instance.CameraManager.transform);
            AppManager.Instance.PlayerManager.DestroyPlayer();
        };
        
        AppManager.Instance.SceneAppManager.SwitchScene("GameOver", 1, callback);
        
        _machine.SwitchState(_machine.NoneFightState);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnExit()
    {
        
    }
}
