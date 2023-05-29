using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class SpriteMovingFightState : State
{
    private Vector3 _velocity = new Vector3(5,5,5);
    private FightAction _fightAction;
    private State _stateAfterMove;
    public SpriteMovingFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _fightAction = _machine.GetBlackboardVariable<FightAction>("fightAction");
        
        _stateAfterMove = _machine.GetBlackboardVariable<State>("stateAfterMove");
    }

    protected override void OnUpdate()
    {
        Vector3 pos = Vector3.SmoothDamp(_fightAction.EntityToMove.transform.position, _fightAction.TargetPos, ref _velocity, 0.2f);
        _fightAction.EntityToMove.transform.position = pos;

        if (Vector3.Distance(_fightAction.EntityToMove.transform.position, _fightAction.TargetPos) <= 0.1f)
        {
            _fightAction.EntityToMove.transform.position = _fightAction.TargetPos;
            _machine.SwitchState(_stateAfterMove);
        }
    }

    protected override void OnExit()
    {
        
    }
}
