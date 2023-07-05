using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAntiscaler : MonoBehaviour
{
    [SerializeField] private RectTransform _this;

    private void Awake()
    {
        _this = GetComponent<RectTransform>();
        Fix();
    }

    private void Fix()
    {
        Vector3 lossy = _this.lossyScale;
        _this.localScale = new Vector3(1 / lossy.x, 1 / lossy.y, 1 / lossy.z);
    }
}
