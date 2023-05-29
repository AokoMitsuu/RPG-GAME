using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Localization;

public class EntitySO : ScriptableObject
{
    public LocalizedString Name => _name;
    [SerializeField] private LocalizedString _name;
    
    public Sprite Sprite => _sprite;
    [SerializeField] private Sprite _sprite;
    
    public Stats Stats => _stats;
    [SerializeField] private Stats _stats;
    
    public List<EntitySkill> EntitySkills => _entitySkills;
    [SerializeField] private List<EntitySkill> _entitySkills;
    
    public AnimatorController BaseAttackAnimationController => _baseAttackAnimationController;
    [SerializeField] private AnimatorController _baseAttackAnimationController;
}

[Serializable]
public struct EntitySkill
{
    public SkillSo Skill;
    public int LevelRequired;
}