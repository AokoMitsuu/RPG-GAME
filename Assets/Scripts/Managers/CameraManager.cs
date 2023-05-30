using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    public void SetCameraTo(Transform transform)
    {
        _camera.transform.SetParent(transform);
        _camera.transform.localPosition = new Vector3(0,0,-10);
    }
}
