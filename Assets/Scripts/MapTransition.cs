using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private MapTransitionSo _mapTransition;
    public bool ChangeMap()
    {
        AppManager.Instance.PlayerManager.SetPlayerInteractable(false);
        System.Action callback = () =>
        {
            AppManager.Instance.PlayerManager.SetPlayerAt(_mapTransition.Position);
            AppManager.Instance.PlayerManager.SetPlayerInteractable(true);
            _mapTransition.SaveLocation();
            AppManager.Instance.SaveLoadManager.Save();
        };
        
        AppManager.Instance.SceneAppManager.SwitchScene(_mapTransition.SceneName, _mapTransition.FadeDuration, callback);
        return true;
    }
}
