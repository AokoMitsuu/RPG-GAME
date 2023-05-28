using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MapTransitionSo _mapTransition;
    
    public void NewGame()
    {
        AppManager.Instance.SaveLoadManager.ResetSave();
        AppManager.Instance.PlayerManager.InitPlayerSO();
        
        System.Action callback = () =>
        {
            AppManager.Instance.PlayerManager.SpawnPlayerAndAttachCamera(_mapTransition.Position);
            _mapTransition.SaveLocation();
            AppManager.Instance.SaveLoadManager.Save();
        };
        
        AppManager.Instance.SceneAppManager.SwitchScene(_mapTransition.SceneName, _mapTransition.FadeDuration, callback);
    }
    
    public void Load()
    {
        AppManager.Instance.SaveLoadManager.Load();
        System.Action callback = () =>
        {
            AppManager.Instance.PlayerManager.SpawnPlayerAndAttachCamera(AppManager.Instance.PlayerManager.PlayerSo.LastPosition);
        };
        
        AppManager.Instance.SceneAppManager.SwitchScene(AppManager.Instance.PlayerManager.PlayerSo.LastSceneName, 1f, callback);
    }
}
