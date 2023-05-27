using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

public class SpriteMovingFightState : State
{
    private Vector3 _velocity = new Vector3(5,5,5);
    private GameObject _objectToMove;
    private Vector3 _endPos;
    private State _stateAfterMove;
    public SpriteMovingFightState(FightStateMachine machine) : base(machine)
    {
    }

    protected override void OnEnter()
    {
        _objectToMove = _machine.GetBlackboardVariable<GameObject>("entityToMove");
        _endPos = _machine.GetBlackboardVariable<Vector3>("endPos");
        _stateAfterMove = _machine.GetBlackboardVariable<State>("stateAfterMove");
    }

    protected override void OnUpdate()
    {
        Vector3 pos = Vector3.SmoothDamp(_objectToMove.transform.position, _endPos, ref _velocity, 0.2f);
        _objectToMove.transform.position = pos;

        if (Vector3.Distance(_objectToMove.transform.position, _endPos) <= 0.01f)
        {
            _objectToMove.transform.position = _endPos;
            _machine.SwitchState(_stateAfterMove);
        }
    }

    protected override void OnExit()
    {
        
    }
}
