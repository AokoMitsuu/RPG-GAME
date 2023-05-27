using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class MapManager : MonoBehaviour
{
    public delegate bool OnEndMovementTickDelegate();
    public OnEndMovementTickDelegate OnEndMovementTick;

    [SerializeField] private LayerMask _encounterLayer;

    private void OnEnable()
    {
        OnEndMovementTick += CheckIfInEncounterZone;
    }

    private void OnDisable()
    {
        OnEndMovementTick -= CheckIfInEncounterZone;
    }

    private bool CheckIfInEncounterZone()
    {
        Collider2D collision = Physics2D.OverlapCircle(AppManager.Instance.PlayerManager.PlayerGo.transform.position, .2f, _encounterLayer);
        
        if (!collision) return false;
        
        if (collision.TryGetComponent(out EncounterZone encounterZone))
        {
            return encounterZone.RollEncounter();
        }
        else if (collision.TryGetComponent(out MapTransition mapTransition))
        {
            return mapTransition.ChangeMap();
        }
        
        return false;
    }
}
