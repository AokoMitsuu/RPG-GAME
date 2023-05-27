using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.UI;

public class UseItemOnAliveHeroFightState : State
{
    private GameObject _itemToUseGameObject;
    private ItemClass _itemToUse;
    private List<HeroFightUI> _heroesFightUI;
    
    public UseItemOnAliveHeroFightState(FightStateMachine machine) : base(machine)
    {
        _heroesFightUI = new List<HeroFightUI>();
    }

    protected override void OnEnter()
    {
        _heroesFightUI.Clear();
        
        _itemToUseGameObject = _machine.GetBlackboardVariable<GameObject>("itemToUseGameObject");
        _itemToUse = _machine.GetBlackboardVariable<ItemClass>("itemToUse");
        _itemToUseGameObject.GetComponent<Image>().sprite = _itemToUse.Sprite;
        
        List<HeroClass> heroes = _machine.GetBlackboardVariable<List<HeroClass>>("heroes");
        foreach (HeroClass hero in heroes)
        {
            hero.HeroFightUI.ToggleHeroItemSelectPanel(true);
            _heroesFightUI.Add(hero.HeroFightUI);
        }
        
        _itemToUseGameObject.SetActive(true);
        Cursor.visible = false;
    }

    protected override void OnUpdate()
    {
        _itemToUseGameObject.transform.position = Input.mousePosition;
    }

    protected override void OnExit()
    {
        _itemToUseGameObject.SetActive(false);
        
        foreach (HeroFightUI heroFightUI in _heroesFightUI)
        {
            heroFightUI.ToggleHeroItemSelectPanel(false);
        }
        
        _heroesFightUI.Clear();
        
        _itemToUseGameObject.SetActive(false);
        Cursor.visible = true;
    }
}
