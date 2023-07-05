using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;

public class DamageUpgradeLoader : UpgradeLoader
{
    public override int Get(Save save)
    {
        return save.GetUpgradeDamage();
    }

    public override void Set(Save save, int value)
    {
        save.SetUpgradeDamage(value);
    }
}
