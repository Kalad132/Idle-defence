using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AdaptiveCamera : MonoBehaviour
{
    private Camera _camera;
    private float _minRatio = 1.3f;
    private float _maxRation = 2.5f;
    private float _minFoV = 50;
    private float _maxFov = 80;


    private void Awake()
    {
        _camera =  GetComponent<Camera>();
        Adapt();
    }

    [ContextMenu("Adapt")]
    private void Adapt()
    {
        float ratio = (float)_camera.pixelHeight / _camera.pixelWidth;
        float lerp = (ratio - _minRatio) / (_maxRation - _minRatio);
        _camera.fieldOfView = Mathf.Lerp(_minFoV, _maxFov, lerp);
    }
}
