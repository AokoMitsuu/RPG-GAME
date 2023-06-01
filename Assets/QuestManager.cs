using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestDataBase QuestDataBase => _questDataBase;
    [SerializeField] private QuestDataBase _questDataBase;

    public Action OnQuestUpdate;

    public bool IsQuestActif(QuestSo questSo)
    {
        return _questDataBase.Quests[AppManager.Instance.PlayerManager.PlayerSo.QuestIndex] == questSo;
    }

    public void QuestComplete(QuestSo questSo)
    {
        if (_questDataBase.Quests[AppManager.Instance.PlayerManager.PlayerSo.QuestIndex] != questSo)
            return;

        AppManager.Instance.PlayerManager.PlayerSo.NextQuest();
        
        OnQuestUpdate?.Invoke();
    }
}
