using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HeroFightUI : MonoBehaviour
{
    [SerializeField] private GameObject _heroItemSelectPanel;
    [SerializeField] private HeroClass _hero;

    public void Init(HeroClass hero)
    {
        _hero = hero;
    }
    
    public void ToggleHeroItemSelectPanel(bool isActive)
    {
        _heroItemSelectPanel.SetActive(isActive);
    }
    
    public void SelectHeroItem()
    {
        AppManager.Instance.FightManager.EntitySelectForItem(_hero);
    }
}
