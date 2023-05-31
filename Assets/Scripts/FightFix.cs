using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightFix : MonoBehaviour
{
    [SerializeField] private DialogueSo _dialogueSo;
    [SerializeField] private Sprite _fightBackground;
    [SerializeField] private ZoneSo.EnemyDataZone enemyDataZone;
    
    public void StartInteraction()
    {
        System.Action afterFight = () =>
        {
            Debug.Log("test");
        };
        
        System.Action startFight = () =>
        {
            AppManager.Instance.FightManager.EnemyEncounter(_fightBackground, AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam, enemyDataZone, afterFight);
        };

        AppManager.Instance.DialogueManager.StartDialogue(_dialogueSo, startFight);
    }
}
