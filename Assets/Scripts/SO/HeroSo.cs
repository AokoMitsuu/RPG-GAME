using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Hero", fileName = "Hero")]
[Serializable]
public class HeroSo : EntitySO
{
    public RuntimeAnimatorController  AnimatorController => _animatorController;
    [SerializeField] private RuntimeAnimatorController  _animatorController;
}
