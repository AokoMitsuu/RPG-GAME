using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Entities/Enemy", fileName = "Enemy")]
public class EnemySo : EntitySO
{
    public int XpDrop => _xpDrop;
    [SerializeField] private int _xpDrop;
}
