using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private UpgradeLoader _loader;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Image _glow;

    private Save _save;
    private bool _selected;

    public event UnityAction<UpgradeView> Selected;

    private void Awake()
    {
        _save = FindObjectOfType<Save>();
        _glow.enabled = false;
    }

    private void Start()
    {
        int upgradeLevel = _loader.Get(_save);
        SetLevelText(upgradeLevel);
    }

    public void Select()
    {
        if (_selected == true)
            return;
        _glow.enabled = true;
        _loader.Set(_save, _loader.Get(_save) + 1);
        _selected = true;
        int upgradeLevel = _loader.Get(_save);
        SetLevelText(upgradeLevel);
        Selected?.Invoke(this);
    }

    public void Deselect()
    {
        if (_selected == false)
            return;
        _glow.enabled = false;
        _loader.Set(_save, _loader.Get(_save) - 1);
        int upgradeLevel = _loader.Get(_save);
        SetLevelText(upgradeLevel);
        _selected = false;
    }

    private void SetLevelText(int value)
    {
        _level.text = value.ToString();
    }
}
