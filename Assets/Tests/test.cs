using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class test : MonoBehaviour
{
    public ReviveItemSo Item;
    public HealItemSo Item2;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReviveItemClass item = new ReviveItemClass(Item);
            HealItemClass item2 = new HealItemClass(Item2);
            AppManager.Instance.PlayerManager.PlayerSo.AddItem(item);
            AppManager.Instance.PlayerManager.PlayerSo.AddItem(item2);
            AppManager.Instance.SaveLoadManager.Save();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(AppManager.Instance.PlayerManager.PlayerSo.Inventory.Count);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam[1].GetHeroName());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam[0].Level);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam[0].GetHeroAttack());
        }
    }
}
