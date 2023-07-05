using Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaggiHP : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Target _target;
    [SerializeField] private float _changeTime;

    private Coroutine _changing;

    private void OnEnable()
    {
        _target.HPchanged += OnHpChanged;
    }

    private void OnDisable()
    {
        _target.HPchanged -= OnHpChanged;
    }

    private void OnHpChanged(float value)
    {
        float target = value / _target.MaxHP;
        if (_changing != null)
            StopCoroutine(_changing);
        _changing = StartCoroutine(Change(target));
    }

    private IEnumerator Change(float target)
    {
        float start = _image.fillAmount;
        float timer = 0;
        while (timer < _changeTime)
        {
            timer += Time.deltaTime;
            _image.fillAmount = Mathf.Lerp(start, target, timer / _changeTime);
            yield return null;
        }
    }
}
