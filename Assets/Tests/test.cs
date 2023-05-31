using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;

public class test : MonoBehaviour
{
    public ReviveItemSo Item;
    public HealItemSo Item2;
    public DialogueSo dialogue;
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     ReviveItemClass item = new ReviveItemClass(Item);
        //     HealItemClass item2 = new HealItemClass(Item2);
        //     
        //     AppManager.Instance.PlayerManager.PlayerSo.AddItem(item);
        //     AppManager.Instance.PlayerManager.PlayerSo.AddItem(item2);
        // }
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     Debug.Log(AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam[1].CurrentLifePoint);
        // }
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (var hero in AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam)
            {
                int levelTo = hero.Level + 1;
                int xp = (levelTo * levelTo * levelTo) - hero.XP;
                hero.AddXp(xp);
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AppManager.Instance.DialogueManager.StartDialogue(dialogue);
        }
    }
}
