using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeViews : MonoBehaviour
{
    private UpgradeView[] _views;

    public event UnityAction<UpgradeView> Selected;

    private void Awake()
    {
        _views = GetComponentsInChildren<UpgradeView>();
    }

    private void OnEnable()
    {
        foreach (UpgradeView view in _views)
            view.Selected += OnViewSelected; 
    }

    private void OnDisable()
    {
        foreach (UpgradeView view in _views)
            view.Selected -= OnViewSelected;
    }

    private void OnViewSelected(UpgradeView selectedView)
    {
        foreach (UpgradeView view in _views)
        {
            if (view != selectedView)
                view.Deselect();
        }
        Selected?.Invoke(selectedView);

    }
}
