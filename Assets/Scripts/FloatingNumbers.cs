using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingNumbers : MonoBehaviour
{
    public static FloatingNumbers Singletron;

    public bool work;

    public FloatingNumber _num;

    private void Awake()
    {
        Singletron = this;
    }

    public void Spawn(Vector3 pos, float val)
    {
        if (work == false)
            return;
        if ((int)val == 0)
            return;
        var num = Instantiate(_num, pos, _num.transform.rotation);
        num.Set(((int)val).ToString());
    }
}
