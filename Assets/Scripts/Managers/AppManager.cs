using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance;

    public FightManager FightManager  { get; private set; }
    public MapManager MapManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public SaveLoadManager SaveLoadManager { get; private set; }
    public SceneAppManager SceneAppManager { get; private set; }
    public CameraManager CameraManager { get; private set; }

    [SerializeField] private GameObject _fightManagerGo;
    [SerializeField] private GameObject _mapManagerGo;
    [SerializeField] private GameObject _playerManagerGo;
    [SerializeField] private GameObject _saveLoadManagerGo;
    [SerializeField] private GameObject _sceneAppManagerGo;
    [SerializeField] private GameObject _CameraManagerGo;
    
    [SerializeField] private string _initialScene;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        string currentScene = SceneManager.GetActiveScene().name;
        
#if UNITY_EDITOR
        if (!SceneManager.GetSceneByName(_initialScene).IsValid()) SceneManager.LoadScene(_initialScene, LoadSceneMode.Additive);
#endif
        
        GameObject playerManagerTmp = Instantiate(_playerManagerGo, transform);
        PlayerManager = playerManagerTmp.GetComponent<PlayerManager>();
        
        GameObject mapManagerTmp = Instantiate(_mapManagerGo, transform);
        MapManager = mapManagerTmp.GetComponent<MapManager>();
        
        GameObject saveManagerTmp = Instantiate(_saveLoadManagerGo, transform);
        SaveLoadManager = saveManagerTmp.GetComponent<SaveLoadManager>();
        
        GameObject fightManagerTmp = Instantiate(_fightManagerGo, transform);
        FightManager = fightManagerTmp.GetComponent<FightManager>();
        
        GameObject sceneAppManagerTmp = Instantiate(_sceneAppManagerGo, transform);
        SceneAppManager = sceneAppManagerTmp.GetComponent<SceneAppManager>();
        SceneAppManager.Init(currentScene);
        
        GameObject cameraManagerTmp = Instantiate(_CameraManagerGo, transform);
        CameraManager = cameraManagerTmp.GetComponent<CameraManager>();
        
        DontDestroyOnLoad(this.gameObject);
    }
}
