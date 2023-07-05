using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

public abstract class UpgradeLoader : MonoBehaviour
{
    public abstract int Get(Save save);

    public abstract void Set(Save save, int value);
}
