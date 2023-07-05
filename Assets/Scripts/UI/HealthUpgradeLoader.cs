using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgradeLoader : UpgradeLoader
{
    public override int Get(Save save)
    {
        return save.GetUpgradeHP();
    }

    public override void Set(Save save, int value)
    {
        save.SetUpgradeHP(value);
    }
}
