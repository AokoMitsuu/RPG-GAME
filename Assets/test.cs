using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class test : MonoBehaviour
{
    public ReviveItemSo Item;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReviveItemClass item = new ReviveItemClass(Item);
            AppManager.Instance.PlayerManager.PlayerSo.AddItem(item);
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
    }
}
