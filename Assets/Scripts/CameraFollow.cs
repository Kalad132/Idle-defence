using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.position - _player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _player.transform.position + _offset, Time.deltaTime * 20f);
    }
}
