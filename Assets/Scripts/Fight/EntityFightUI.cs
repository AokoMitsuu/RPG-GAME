using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EntityFightUI : MonoBehaviour
{
    [SerializeField] private GameObject _targetArrow;
    private EntityClass _entityClass;

    public void Init(EntityClass entity)
    {
        _entityClass = entity;
    }

    public void ToggleTargetPanel(bool isActive)
    {
        _targetArrow.SetActive(isActive);
    }

    public void SelectTarget()
    {
        AppManager.Instance.FightManager.SelectTarget(_entityClass);
    }
}
