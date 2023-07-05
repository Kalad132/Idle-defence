using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumber : MonoBehaviour
{
    public TMP_Text text;
    public float _speed;

    public void Set(string text)
    {
        this.text.text = text;
        Invoke(nameof(Destroy), 0.5f);
    }

    private void Update()
    {
        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
