using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapTransition", fileName = "MapTransition")]
public class MapTransitionSo : ScriptableObject
{
    public string SceneName => _sceneName;
    [SerializeField] private string _sceneName;
    
    public Vector3 Position => _position;
    [SerializeField] private Vector3 _position;
    
    public float FadeDuration => _fadeDuration;
    [SerializeField] private float _fadeDuration;

    private void OnValidate()
    {
        if (_fadeDuration <= 0)
        {
            _fadeDuration = 0.1f;
        }
    }

    public void SaveLocation()
    {
        AppManager.Instance.PlayerManager.PlayerSo.SetLastSceneName(_sceneName);
        AppManager.Instance.PlayerManager.PlayerSo.SetLastPosition(_position);
    }
}
