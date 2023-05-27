using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EncounterZone : MonoBehaviour
{
    [SerializeField] private ZoneSo _zoneSo;

    private void Start()
    {
        if (_zoneSo == null) return;
        
        _zoneSo.Init();
    }

    public bool RollEncounter()
    {
        if (_zoneSo == null) return false;
        
        if (Random.Range(1, 101) > _zoneSo.Rate) return false;
        
        var range = _zoneSo.EnemyTotalRates;
        var random = Random.Range(1, range);
        
        for (var i = 0; i < _zoneSo.EnemyDataZones.Count; i++)
        {
            range -= _zoneSo.EnemyDataZones[i].Rate;
            
            if (random <= range) continue;

            AppManager.Instance.PlayerManager.SetPlayerMovable(false);
            AppManager.Instance.FightManager.StartFight(_zoneSo.Background, AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam, _zoneSo.EnemyDataZones[i]);
            break;
        }

        return true;
    }
}
