using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReviveItemClass : ItemClass
{
    public int Heal => _heal;
    [SerializeField] private int _heal;

    public ReviveItemClass(ReviveItemSo healItem) : base(healItem)
    {
        _heal = healItem.Heal;
    }

    public override void UseEffect(HeroClass hero)
    {
        hero.Heal(_heal);
        AppManager.Instance.FightManager.ReviveHero(hero);
        base.UseEffect(hero);
    }
}
