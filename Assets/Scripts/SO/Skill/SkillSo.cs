using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class SkillSo : ScriptableObject
{
    public int Cost => _cost;
    [SerializeField] private int _cost;
    
    public SkillTarget SkillTarget => _skillTarget;
    [SerializeField] private SkillTarget _skillTarget;   
    
    public SkillType SkillType => _skillType;
    [SerializeField] private SkillType _skillType;
    
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public LocalizedString Name => _name;
    [SerializeField] private LocalizedString _name;
    
    public RuntimeAnimatorController  AnimatorController => _animatorController;
    [SerializeField] private RuntimeAnimatorController  _animatorController;
    
    public virtual void UseEffect(EntityClass entityAction, EntityClass entityTarget)
    {
        entityAction.ConsumeMana(_cost);
    }
}

public enum SkillTarget
{
    Enemy,
    AliveHero,
    DeadHero
}

public enum SkillType{
    Heal,Damage
}