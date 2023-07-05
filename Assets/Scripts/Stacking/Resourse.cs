using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourse : MonoBehaviour
{
    public const float AnimationTime = 0.5f;

    [SerializeField] private int _id;
    [SerializeField] private Vector3 _size;

    private Coroutine _animating;
    private Transform _animationTarget;

    public ResourseId ID => new ResourseId(_id);
    public Vector3 Size => _size;

    private void OnDisable()
    {
        if (_animating != null)
            StopCoroutine(_animating);
        if (_animationTarget != null)
        {
            Destroy(_animationTarget.gameObject);
            _animationTarget = null;
        }
    }

    public void AnimateMove(Transform target, Vector3 offset, Quaternion rotationOffset, bool fast = false)
    {
        if (_animating != null)
            StopCoroutine(_animating);
        if (_animationTarget != null)
        {
            Destroy(_animationTarget.gameObject);
            _animationTarget = null;
        }
        _animating = StartCoroutine(Animate(target, offset, rotationOffset, fast));
    }

    private IEnumerator Animate(Transform target, Vector3 offset, Quaternion rotationOffset, bool fast)
    {
        _animationTarget = new GameObject("animation target").transform;
        _animationTarget.parent = target.transform;
        _animationTarget.transform.localPosition = offset;
        _animationTarget.rotation = target.transform.rotation * rotationOffset;
        float timer = 0;
        float time = AnimationTime;
        if (fast)
            time = 0.3f;
        Vector3 start = transform.position;
        while (timer <= time && _animationTarget != null)
        {
            timer += Time.deltaTime;
            float progress = timer / time;
            transform.position = Vector3.Lerp(start, _animationTarget.position, progress);
            if (fast == false)
                transform.position += CalculateAdditionalOffset(progress);
            transform.rotation = Quaternion.Lerp(transform.rotation, _animationTarget.rotation, progress);
            yield return null;
        }
        if (_animating != null)
            StopCoroutine(_animating);
        if (_animationTarget != null)
        {
            Destroy(_animationTarget.gameObject);
            _animationTarget = null;
        }
    }

    private Vector3 CalculateAdditionalOffset(float progress)
    {
        progress = Mathf.Clamp(progress, 0, 1);
        return Vector3.up * (-Mathf.Abs(progress - 0.5f)*2 +1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position + Vector3.up*Size.y/2, Size);
    }
}
