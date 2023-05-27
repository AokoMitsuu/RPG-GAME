using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Load()
    {
        AppManager.Instance.SaveLoadManager.Load();
        System.Action callback = () =>
        {
            AppManager.Instance.PlayerManager.SpawnPlayerAndAttachCamera(AppManager.Instance.PlayerManager.PlayerSo.LastPosition);
        };
        
        AppManager.Instance.SceneAppManager.SwitchScene(AppManager.Instance.PlayerManager.PlayerSo.LastSceneName, 1f, callback);
    }

    public void MainMenu()
    {
        System.Action callback = () => {};
        
        AppManager.Instance.SceneAppManager.SwitchScene("MainMenu", 1, callback);
    }
}
