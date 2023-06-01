using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

    public FightManager FightManager  { get; private set; }
    public MapManager MapManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public SaveLoadManager SaveLoadManager { get; private set; }
    public SceneAppManager SceneAppManager { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public DialogueManager DialogueManager { get; private set; }
    public QuestManager QuestManager { get; private set; }

    [SerializeField] private GameObject _fightManagerGo;
    [SerializeField] private GameObject _mapManagerGo;
    [SerializeField] private GameObject _playerManagerGo;
    [SerializeField] private GameObject _saveLoadManagerGo;
    [SerializeField] private GameObject _sceneAppManagerGo;
    [SerializeField] private GameObject _cameraManagerGo;
    [SerializeField] private GameObject _dialogueManagerGo;
    [SerializeField] private GameObject _questManager;
    
    [SerializeField] private string _initialScene;
    [SerializeField] private string firstScene;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        string currentScene = SceneManager.GetActiveScene().name;
        
        if (!SceneManager.GetSceneByName(_initialScene).IsValid()) SceneManager.LoadScene(_initialScene, LoadSceneMode.Additive);
        
        
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
        
        GameObject cameraManagerTmp = Instantiate(_cameraManagerGo, transform);
        CameraManager = cameraManagerTmp.GetComponent<CameraManager>();
        
        GameObject dialogueManagerTmp = Instantiate(_dialogueManagerGo, transform);
        DialogueManager = dialogueManagerTmp.GetComponent<DialogueManager>();
        
        GameObject questMangerTmp = Instantiate(_questManager, transform);
        QuestManager = questMangerTmp.GetComponent<QuestManager>();
        
        DontDestroyOnLoad(this.gameObject);
        
        
    }
    
#if UNITY_EDITOR
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("PlayerStart"))
        {
            SaveLoadManager.Load();
            PlayerManager.SpawnPlayerAndAttachCamera(GameObject.FindGameObjectWithTag("PlayerStart").transform.position);
        }
    }
#endif

}
