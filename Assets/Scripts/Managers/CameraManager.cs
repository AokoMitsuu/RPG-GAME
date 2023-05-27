using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    public void SetCameraTo(Transform transfrom)
    {
        _camera.transform.SetParent(transfrom);
        _camera.transform.position = new Vector3(0,0,-10);
    }
}
