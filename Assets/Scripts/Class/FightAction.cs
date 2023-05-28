using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;


public class FightAction
{
    public int Damage;
    public int Heal;
    public int Cost;
    public AnimatorController AnimatorController;
    public GameObject EntityToMove;
    public EntityClass EntityAction;
    public EntityClass EntityTarget;
    public Vector3 EntityInitalPos;
    public Vector3 TargetPos;
    public FightActionType FightActionType;
}

public enum FightActionType{
    Attack,
    Heal,
    Revive
}
