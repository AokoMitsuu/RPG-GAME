using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerSo PlayerSo => _playerSo;
    [SerializeField] private PlayerSo _playerSo;
    
    public GameObject PlayerPrefabs => _playerPrefabs;
    [SerializeField] private GameObject _playerPrefabs;
    
    public GameObject PlayerGo => _playerGo;
    [SerializeField] private GameObject _playerGo;
    
    private PlayerMovement _playerMovement;
    
    private PlayerInteraction _playerInteraction;
    
    public HeroDataBase HeroDataBase => _heroDataBase;
    [SerializeField] private HeroDataBase _heroDataBase;

    public void InitPlayerSO()
    {
        _playerSo.Init();
    }

    public void LoadPlayerSo(string playerSo)
    {
        _playerSo = ScriptableObject.CreateInstance<PlayerSo>();
        JsonUtility.FromJsonOverwrite(playerSo, _playerSo);
        _playerSo.Load();
    }

    public void SpawnPlayerAndAttachCamera(Vector3 position)
    {
        SetplayerGameObjectInstance(Instantiate(PlayerPrefabs, position, quaternion.identity));
        AppManager.Instance.CameraManager.SetCameraTo(PlayerGo.transform);
    }
    
    public void SetPlayerAt(Vector3 position)
    {
        _playerMovement.SetPosition(position);
    }
    
    public void SetplayerGameObjectInstance(GameObject PlayerGoInstance)
    {
        _playerGo = PlayerGoInstance;
        _playerMovement = _playerGo.GetComponent<PlayerMovement>();
        _playerInteraction = _playerGo.GetComponent<PlayerInteraction>();
    }

    public void DestroyPlayer()
    {
        Destroy(_playerGo);
    }

    public void SetPlayerInteractable(bool canInteract)
    {
        if (_playerMovement == null) return;
        
        _playerMovement.SetPlayerMovable(canInteract);
        _playerInteraction.SetPlayerInteractable(canInteract);
    }
}
