using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Entities/Hero", fileName = "Hero")]
[Serializable]
public class HeroSo : ScriptableObject
{
    public LocalizedString Name => _name;
    [SerializeField] private LocalizedString _name;
    public Sprite FightSprite => _fightSprite;
    [SerializeField] private Sprite _fightSprite;
    public AnimatorController AnimatorController => _animatorController;
    [SerializeField] private AnimatorController _animatorController;
    
    public Stats Stats => _stats;
    [SerializeField] private Stats _stats;
}
