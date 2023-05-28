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
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam[1].CurrentLifePoint);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var VARIABLE in AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam)
            {
                VARIABLE.AddXp(217);
            }
        }
    }
}
