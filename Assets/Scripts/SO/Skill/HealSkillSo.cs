using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Heal", fileName = "Heal")]
public class HealSkillSo : SkillSo
{
    public int Heal => _heal;
    [SerializeField] private int _heal;
    
    public override void UseEffect(EntityClass entityAction, EntityClass entityTarget)
    {
        base.UseEffect(entityAction, entityTarget);
        ((HeroClass)entityTarget).Heal(_heal);
    }
}
