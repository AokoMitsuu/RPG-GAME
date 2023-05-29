using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSkillSo : SkillSo
{
    public int Damage => _damage;
    [SerializeField] private int _damage;
    
    public override void UseEffect(EntityClass entityAction, EntityClass entityTarget)
    {
        base.UseEffect(entityAction, entityTarget);
        entityTarget.TakeDamage(entityAction.GetAttack() + Damage);
    }
}
