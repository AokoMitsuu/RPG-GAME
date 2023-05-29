using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Fire", fileName = "Fire")]
public class FireSkillSo : DamageSkillSo
{
    public override void UseEffect(EntityClass entityAction, EntityClass entityTarget)
    {
        base.UseEffect(entityAction, entityTarget);
    }
}
