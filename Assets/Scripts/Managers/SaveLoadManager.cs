using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private bool disableSave;
    
    [SerializeField] private SaveData _data;
    public SaveData Data => _data;

    private void SetSaveData()
    {
        _data.SetPlayerSo(AppManager.Instance.PlayerManager.PlayerSo);
    }

    public void Save()
    {
        if (disableSave) return;
        
        SetSaveData();
        
        BinaryFormatter formatter = new BinaryFormatter();
        
        string path = Application.persistentDataPath + "/Player.save";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        formatter.Serialize(stream, Data);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/Player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            _data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            
            AppManager.Instance.PlayerManager.LoadPlayerSo(_data.PlayerSo);
        }
        else
        {
            _data = new SaveData();
            Save();
        }
    }

    public void ResetSave()
    {
        _data = new SaveData();
        Save();
    }
}

[Serializable]
public class SaveData
{
    public string PlayerSo => _playerSo;
    public string _playerSo;

    public void SetPlayerSo(PlayerSo playerSo)
    {
        _playerSo = JsonUtility.ToJson(playerSo);
    }
}