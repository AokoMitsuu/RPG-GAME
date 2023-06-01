using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightFix : MonoBehaviour
{
    [SerializeField] private DialogueSo _dialogueSo;
    [SerializeField] private Sprite _fightBackground;
    [SerializeField] private ZoneSo.EnemyDataZone _enemyDataZone;
    [SerializeField] private QuestSo _quest;

    private void OnEnable()
    {
        AppManager.Instance.QuestManager.OnQuestUpdate += ActiveIfQuestActive;
    }

    private void Start()
    {
        ActiveIfQuestActive();
    }

    public void StartInteraction()
    {
        System.Action afterFight = () =>
        {
            AppManager.Instance.QuestManager.QuestComplete(_quest);
        };
        
        System.Action startFight = () =>
        {
            AppManager.Instance.FightManager.EnemyEncounter(_fightBackground, AppManager.Instance.PlayerManager.PlayerSo.HeroesTeam, _enemyDataZone, afterFight);
        };

        AppManager.Instance.DialogueManager.StartDialogue(_dialogueSo, startFight);
    }

    public void ActiveIfQuestActive()
    {
        gameObject.SetActive(AppManager.Instance.QuestManager.IsQuestActif(_quest));
    }
}
