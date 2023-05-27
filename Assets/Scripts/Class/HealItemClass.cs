using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemClass : ItemClass
{
    public int Heal => _heal;
    [SerializeField] private int _heal;

    public HealItemClass(HealItemSo healItem) : base(healItem)
    {
        _heal = healItem.Heal;
    }

    public override void UseEffect(HeroClass hero)
    {
        hero.Heal(_heal);
        
        base.UseEffect(hero);
    }
}
