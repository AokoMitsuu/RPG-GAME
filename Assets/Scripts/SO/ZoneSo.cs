using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Zone/Zone", fileName = "Zone")]
public class ZoneSo : ScriptableObject
{
    public Sprite Background => _background;
    [SerializeField] private Sprite _background;
    
    public int Rate => _rate;
    [SerializeField, Range(0,100)] private int _rate;
    
    public List<EnemyDataZone> EnemyDataZones => _enemyDataZones;
    [SerializeField] private List<EnemyDataZone> _enemyDataZones;
    
    public int EnemyTotalRates => _enemyTotalRates;
    [SerializeField] private int _enemyTotalRates;

    public void Init()
    {
        _enemyTotalRates = 0;
        foreach (EnemyDataZone enemyData in _enemyDataZones)
        {
            _enemyTotalRates += enemyData.Rate;
        }
    }
    
    [Serializable]
    public struct EnemyDataZone
    {
        public EnemyGroupSo EnemyGroup;
        public int Rate;
        public FightTransition FightTransition;
    }

    [Serializable]
    public struct FightTransition
    {
        public Material Material;
        public float Duration;
    }
}
