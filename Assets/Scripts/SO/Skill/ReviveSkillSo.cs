using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Revive", fileName = "Revive")]
public class ReviveSkillSo : HealSkillSo
{
    public override void UseEffect(EntityClass entityAction, EntityClass entityTarget)
    {
        base.UseEffect(entityAction, entityTarget);
        AppManager.Instance.FightManager.ReviveHero((HeroClass)entityTarget);
    }
}
