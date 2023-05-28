using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Hero", fileName = "Hero")]
[Serializable]
public class HeroSo : EntitySO
{
    public AnimatorController AnimatorController => _animatorController;
    [SerializeField] private AnimatorController _animatorController;
}
