using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using UnityEngine.UI;

public class UseItemOnDeadHeroFightState : State
{
    private GameObject _itemToUseGameObject;
    private ItemClass _itemToUse;
    private List<HeroFightUI> _heroesFightUI;
    
    public UseItemOnDeadHeroFightState(FightStateMachine machine) : base(machine)
    {
        _heroesFightUI = new List<HeroFightUI>();
    }

    protected override void OnEnter()
    {
        _heroesFightUI.Clear();
        
        _itemToUseGameObject = _machine.GetBlackboardVariable<GameObject>("itemToUseGameObject");
        _itemToUse = _machine.GetBlackboardVariable<ItemClass>("itemToUse");
        _itemToUseGameObject.GetComponent<Image>().sprite = _itemToUse.Sprite;
        
        List<HeroClass> heroesDead = _machine.GetBlackboardVariable<List<HeroClass>>("heroesDead");
        foreach (HeroClass hero in heroesDead)
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
