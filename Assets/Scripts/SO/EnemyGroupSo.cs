using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zone/EnemyGroup",fileName = "EnemyGroup")]
public class EnemyGroupSo : ScriptableObject
{
    public EnemyData[] Enemy;

    private void OnValidate()
    {
        if (Enemy.Length != 9)
        {
            Array.Resize(ref Enemy, 9);
        }
    }

    [Serializable]
    public struct EnemyData
    {
        public EnemySo Enemy;
        public int MinLevel;
        public int MaxLevel;
    }
}
