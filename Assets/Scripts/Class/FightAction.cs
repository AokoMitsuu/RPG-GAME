using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightAction
{
    public int Damage;
    public int Heal;
    public int Cost;
    public RuntimeAnimatorController AnimatorController;
    public GameObject EntityToMove;
    public EntityClass EntityAction;
    public EntityClass EntityTarget;
    public Vector3 EntityInitalPos;
    public Vector3 TargetPos;
    public FightActionType FightActionType;
    public ItemClass Item;
    public SkillSo Skill;
}

public enum FightActionType{
    Attack,
    HealItem,
    ReviveItem,
    Skill,
}
