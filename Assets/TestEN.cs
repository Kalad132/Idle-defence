using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Enemies;
using Shooting;

public class TestEN : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private AllEnemies en;
    [SerializeField] private AllTargets tar;

    private void Update()
    {
        text.text = en.Count.ToString() + "/" + tar.All.Count.ToString();
    }
}
