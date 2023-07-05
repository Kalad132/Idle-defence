using Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsUpgradeLoader : UpgradeLoader
{
    public override int Get(Save save)
    {
        return save.GetUpgradeHands();
    }

    public override void Set(Save save, int value)
    {
        save.SetUpgradeHands(value);
    }
}
