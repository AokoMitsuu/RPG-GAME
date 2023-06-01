using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistIfQuest : MonoBehaviour
{
    [SerializeField] private QuestSo _quest;
    
    public void ActiveIfQuestActive() => gameObject.SetActive(AppManager.Instance.QuestManager.IsQuestActif(_quest));
    private void OnEnable()
    {
        AppManager.Instance.QuestManager.OnQuestUpdate += ActiveIfQuestActive;
    }
    
    private void Start()
    {
        ActiveIfQuestActive();
    }
    
}
